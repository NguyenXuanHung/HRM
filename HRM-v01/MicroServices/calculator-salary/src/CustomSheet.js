import React, { PureComponent } from 'react'
import { Parser } from 'hot-formula-parser'
import axios from 'axios'
import mathjs from 'mathjs'
import _ from 'lodash'
import XLSX from 'xlsx'
import { FormattedNumber } from 'react-intl'
import format from 'string-format'
import withFixedColumns from 'react-table-hoc-fixed-columns'
import ReactTable from 'react-table'
import 'react-table/react-table.css'

const parser = new Parser()

const url =
  process.env.REACT_APP_SERVICE_URL ||
  '/Services/HandlerSalaryBoardSheet.ashx'
const dataUrl = '/Services/HandlerSalaryBoardData.ashx'

const defaultColName = [
  {
    value: 'ID',
    key: 'A1',
    readOnly: true,
    expr: '',
    className: 'title',
    columnCode: 'Id',
    isShow: false
  },
  {
    value: 'RecordID',
    key: 'B1',
    readOnly: true,
    expr: '',
    className: 'title',
    columnCode: 'RecordId',
    isShow: false
  },
  {
    value: 'Họ Tên',
    key: 'C1',
    readOnly: true,
    expr: '',
    className: 'title',
    columnCode: 'FullName',
    isShow: true
  },
  {
    value: 'Mã Nhân Viên',
    key: 'D1',
    readOnly: true,
    expr: '',
    className: 'title',
    columnCode: 'EmployeeCode',
    isShow: true
  },
  {
    value: 'Chức vụ',
    key: 'E1',
    readOnly: true,
    expr: '',
    className: 'title',
    columnCode: 'Position',
    isShow: true
  }
]

const defaultCol = [
  { label: 'A', readOnly: true },
  { label: 'B', readOnly: true },
  { label: 'C', readOnly: true },
  { label: 'D', readOnly: true },
  { label: 'E', readOnly: true }
]

const ReactTableFixedColumns = withFixedColumns(ReactTable)

class CustomSheet extends PureComponent {
  constructor(props) {
    super(props)
    this.state = {
      grid: [],
      headers: [],
      gridArray: [],
      gridConfig: []
    }
    format.extend(String.prototype, {})
  }

  componentDidMount() {
    const search = this.props.location.search
    let salaryBoardId = search.slice(search.indexOf('=') + 1, search.length)
    this.requestSalaryBoardId(salaryBoardId)
  }

  requestSalaryBoardId = salaryBoardId => {
    let requestUrl = `${url}?id=${salaryBoardId}`
    axios
      .get(requestUrl)
      .then(res => {
        var { data } = res
        console.log(res.data)
        this.setState({
          gridConfig: data.SalaryBoardConfigs
        })
        this.setState({
          grid: this.generateGrid(data.SalaryBoardDynamics),
          headers: this.generateHeaders()
        })
        this.createArrayFromGrid()
        this.updateSheet()
        console.log('state...', this.state)
      })
      .then(() => {
        const { grid } = this.state
        axios
          .post(dataUrl, {
            SalaryBoardId: salaryBoardId,
            Data: grid
          })
          .catch(err => console.log(err))
      })
      .catch(err => console.log(err))
  }

  // Tạo header
  generateHeaders = () => {
    let headers = [...defaultCol]
    this.state.gridConfig.forEach((colConfig, i) => {
      headers.push({ label: colConfig.ColumnExcel, readOnly: true })
    })
    return headers
  }

  // Tạo grid
  generateGrid = json => {
    let grid = []
    let colName = [...defaultColName]
    this.state.gridConfig.forEach(colConfig => {
      colName.push({
        value: colConfig.Display,
        key: colConfig.ColumnExcel + 1,
        readOnly: true,
        expr: '',
        className: 'title'
      })
    })

    grid.push(colName)

    json.forEach((row, i) => {
      let cells = [
        {
          value: row['Id'],
          key: 'A' + (i + 2),
          readOnly: true,
          expr: '',
          prop: 'Id'
        },
        {
          value: row['RecordId'],
          key: 'B' + (i + 2),
          readOnly: true,
          expr: '',
          prop: 'RecordId'
        },
        {
          value: row['FullName'],
          key: 'C' + (i + 2),
          readOnly: true,
          expr: '',
          prop: 'FullName'
        },
        {
          value: row['EmployeeCode'],
          key: 'D' + (i + 2),
          readOnly: true,
          expr: '',
          prop: 'EmployeeCode'
        },
        {
          value: row['PositionName'],
          key: 'E' + (i + 2),
          readOnly: true,
          expr: '',
          prop: 'PositionName'
        }
      ]

      Object.keys(row).forEach((key, j) => {
        this.state.gridConfig.forEach(colConfig => {
          if (colConfig.Display === key) {
            let cell = row[key]
            cells.push({
              value: cell,
              key: colConfig.ColumnExcel + (i + 2),
              readOnly: colConfig.IsReadOnly,
              expr: colConfig.Formula.format(i + 2),
              prop: colConfig.ColumnCode
            })
          }
        })
      })
      grid.push(cells)
    })
    return grid
  }

  // Tạo array từ grid
  createArrayFromGrid = () => {
    var gridArray = []
    for (var row = 0; row < this.state.grid.length; row++) {
      var colExcel = []
      for (var col = 0; col < this.state.headers.length; col++) {
        let cell = this.state.grid[row][col].value
        colExcel.push(cell)
      }
      gridArray.push(colExcel)
    }
    this.setState({ gridArray })
  }

  // Kiểm tra công thức
  validateExp = (trailKeys, expr) => {
    let valid = true
    const matches = expr.match(/[A-Z][1-9]+/g) || []
    matches.forEach(match => {
      if (trailKeys.indexOf(match) > -1) {
        valid = false
      } else {
        this.state.grid.forEach(row => {
          Object.keys(row).forEach((key, j) => {
            if (row[key].key === match) {
              valid = this.validateExp([...trailKeys, match], row[key].expr)
            }
          })
        })
      }
    })
    return valid
  }

  computeExpr = (key, expr, scope) => {
    // console.log('key', key)
    // console.log('expr', expr)
    // console.log('scope', scope)
    let value = null

    var data = this.state.gridArray

    parser.on('callCellValue', function(cellCoord, done) {
      let result = 0
      if(data[cellCoord.row.index][cellCoord.column.index])
        result = data[cellCoord.row.index][cellCoord.column.index]
      done(result)
    })

    parser.on('callRangeValue', function(startCellCoord, endCellCoord, done) {
      var excelArr = []

      for (
        var row = startCellCoord.row.index;
        row <= endCellCoord.row.index;
        row++
      ) {
        var rowData = data[row]
        var colExcel = []

        for (
          var col = startCellCoord.column.index;
          col <= endCellCoord.column.index;
          col++
        ) {
          colExcel.push(rowData[col])
        }
        excelArr.push(colExcel)
      }

      if (excelArr) {
        done(excelArr)
      }
    })
    
    if (expr.charAt(0) !== '=') {
      let value
      if (expr !== '' || expr != null) {
        value = parseFloat(expr)
      } else {
        value = ''
      }
      return { className: '', value: isNaN(value) ? '' : value, expr: expr }
    } else {
      try {
        const valueReturn = parser.parse(expr.substring(1))
        // console.log("valReturn", valueReturn)
        value = mathjs.eval(valueReturn.result, scope)
      } catch (e) {
        value = null
      }
      // console.log('[key] ---', [key])
      if (value !== null && this.validateExp([key], expr)) {
        return { className: 'equation', value, expr }
      } else {
        return { className: 'error', value: 'error', expr }
      }
    }
  }

  cellUpdate = (state, changeCell, expr) => {
    const scope = _.mapValues(
      state.grid,
      val => (isNaN(val.value) ? 0 : parseFloat(val.value))
    )
    const updatedCell = _.assign(
      {},
      changeCell,
      this.computeExpr(changeCell.key, expr, scope)
    )

    state.grid.forEach((row, i) => {
      Object.keys(row).forEach((key, j) => {
        let cell = row[key]
        if (cell.key === changeCell.key) {
          state.grid[i][j] = updatedCell
        }
        if (
          cell.expr.charAt(0) === '=' &&
          cell.expr.indexOf(changeCell.key) > -1 &&
          cell.key !== changeCell.key
        ) {
          state.grid = this.cellUpdate(state.grid, cell, cell.expr)
        }
      })
    })
    // console.log('updatedCell...', updatedCell);
    // console.log('state...', state);
    return state
  }

  onCellsChanged = changes => {
    const state = _.assign({}, this.state)

    // console.log('old state...', state)
    // console.log('changes...', changes)
    changes.forEach(({ cell, value }) => {
      this.cellUpdate(state, cell, value)
    })

    this.setState(state)
    this.createArrayFromGrid()
    // console.log('state after change ....', state)
  }

  updateSheet = () => {
    const state = _.assign({}, this.state)
    this.state.grid.forEach(row => {
      Object.keys(row).forEach(key => {
        let cell = row[key]
        if (cell.expr !== '') {
          this.cellUpdate(state, cell, cell.expr)
        }
      })
    })
    this.setState(state)
    this.createArrayFromGrid()
  }

  exportFile = () => {
    /* convert state to workbook */
    const ws = XLSX.utils.aoa_to_sheet(this.state.gridArray)
    const wb = XLSX.utils.book_new()
    XLSX.utils.book_append_sheet(wb, ws, 'SalaryBoard')
    /* generate XLSX file and send to client */
    XLSX.writeFile(wb, 'SalaryBoard.xlsx')
  }

  getColumnHeader = () => {
    const { headers, grid } = this.state
    let headerColumn = []
    if (grid && grid.length > 0) {
      const headerGrid = grid[0]
      headers.forEach(x => {
        let index = _.indexOf(headers, x, 0)
        let header = headerGrid[index]
        if (header) {
          headerColumn.push({
            ...x,
            name: header.value
          })
        }
      })
    }
    return headerColumn
  }

  renderFixColumn = fixedColumn => {
    return (
      <div className="row">
        <div style={{ width: 45 }} />
        {fixedColumn &&
          fixedColumn.length > 0 &&
          fixedColumn.map((x, index) => (
            <div style={{ width: 197 }} key={index}>
              {x.columnName}
            </div>
          ))}
      </div>
    )
  }

  render() {
    const { grid, gridConfig } = this.state
    let gridCopy = [...grid]
    let gridConfigFullColumn = []

    const fixedColumn = []

    let defaultColumn = _.filter(defaultColName, x => x.isShow)
    if (defaultColumn && defaultColumn.length > 0) {
      defaultColumn.forEach(x => {
        fixedColumn.push({
          columnCode: x.columnCode,
          columnName: x.key.replace(/\d+/g, ''),
          Header: x.value,
          fixed: 'left',
          width: 197,
          accessor: x.columnCode
        })
      })
    }

    gridConfigFullColumn.push({
      columnCode: 'index',
      columnName: 'index',
      fixed: 'left',
      Header: this.renderFixColumn(fixedColumn),
      columns: [
        {
          Header: 'STT',
          width: 45,
          accessor: 'index',
          columnName: 'index'
        },
        ...fixedColumn
      ]
    })

    gridConfig.forEach(x => {
      gridConfigFullColumn.push({
        Header: x.ColumnExcel.replace(/\d+/g, ''),
        columns: [
          {
            Header: x.Display,
            width: 197,
            accessor: x.ColumnCode,
            columnCode: x.ColumnCode,
            columnName: x.ColumnExcel.replace(/\d+/g, '')
          }
        ]
      })
    })

    let data = []
    gridCopy.shift()
    gridCopy.forEach((row, index) => {
      let object = { index: index + 1 }
      row.forEach(cell => {
        let column = cell.key.replace(/\d+/g, '')
        let propsName = null
        gridConfigFullColumn.forEach(x => {
          if (x.columns && x.columns.length > 0) {
            let mapItems = _.find(x.columns, y => y.columnName === column)
            if (mapItems) {
              propsName = mapItems.columnCode
              return
            }
          }
        })

        let value = _.isNumber(cell.value)
        if (propsName) {
          _.set(
            object,
            propsName,
            value ? <FormattedNumber value={cell.value} /> : cell.value
          )
        }
      })
      data.push(object)
    })
    return (
      <div>
        <ReactTableFixedColumns
          data={data}
          columns={gridConfigFullColumn}
          style={{ height: 500 }}
        />
        <button
          className="btn btn-info mt-3 btn-export"
          onClick={this.exportFile}>
          Xuất ra Excel
        </button>
      </div>
    )
  }
}

export default CustomSheet
