import React, { useEffect } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Home } from "./pages/Home";
import { UI } from "./pages/UI";
import { Vacancies } from "./pages/Vacancies";
import { Test } from "./pages/Test";
import { useAppDispatch } from "./hooks/redux";
import { fetchCurrentUser } from "./store/actions/userActionCreators";
import { Registration } from "./pages/Registration";
import { Login } from "./pages/Login";

function App() {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchCurrentUser());
  }, []);

  return (
    <Router>
      <div>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/UI" element={<UI />}/>
          <Route path="/vacancies" element={<Vacancies />} />
          <Route path="/test" element={<Test />} />
          <Route path="/registration" element={<Registration />} />
          <Route path="/login" element={<Login />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
