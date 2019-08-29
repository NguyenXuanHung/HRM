import React, { Component } from 'react'

// import 'react-select/dist/react-select.css'
import './index.css'
import 'react-datasheet/lib/react-datasheet.css'
import SheetRouter from './SheetRouter'

class App extends Component {
  render() {
    return (
      <div>
        <SheetRouter />
      </div>
    )
  }
}

export default App
