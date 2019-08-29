import React from 'react'
import ReactDOM from 'react-dom'
import RootContainer from './RootContainer'
import registerServiceWorker from './registerServiceWorker'

ReactDOM.render(<RootContainer />, document.getElementById('root'))
registerServiceWorker()
