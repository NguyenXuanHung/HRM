import React from 'react'
import App from './App'
import { IntlProvider } from 'react-intl'

const RootContainer = () => (
  <IntlProvider locale="en">
    <App />
  </IntlProvider>
)

export default RootContainer
