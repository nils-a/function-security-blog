import * as React from "react";
import { Route, HashRouter as Router, Redirect, Switch, NavLink } from "react-router-dom";

import { Demo1 } from "./Demo1"

import "../app.scss";

export interface AppProps {
}

export interface OrderMatchParams {
    filter: string;
}

export const Routes = {
    Demo1: "/Demo1",
}

export const App = (props: AppProps) => (
    <Router>
            <div>
                <nav>
                    <NavLink to={Routes.Demo1} activeClassName="active">Demo1</NavLink>
                </nav>
                <Switch>
                    <Redirect from="/" exact={true} to={Routes.Demo1} />
                    <Route name={Routes.Demo1} path={Routes.Demo1}>
                        <Demo1  />
                    </Route>
                </Switch>
            </div>
    </Router>
);
