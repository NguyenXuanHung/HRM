import React from 'react'
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import CustomSheet from './CustomSheet'

export default class SheetRouter extends React.Component {
  render() {
    return (
      <BrowserRouter>
        <div>
          <Switch>
            <Route exact path="/" component={CustomSheet} />
            <Route path="/:id" component={CustomSheet} />
          </Switch>
        </div>
      </BrowserRouter>
    )
  }
}
