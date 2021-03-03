import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { NewQuestion } from './components/NewQuestion';
import { Questions } from './components/Questions';
import { Question } from './components/Question';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Login} />
            <Route exact path='/newQuestion' component={NewQuestion} />
            <Route exact path='/questions' component={Questions} />
            <Route exact path='/questions/topic/:top' component={Questions} />
            <Route exact path='/questions/:filter' component={Questions} />
            <Route exact path='/question/:id' component={Question} />
      </Layout>
    );
  }
}
