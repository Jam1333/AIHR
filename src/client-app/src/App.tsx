import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Home } from "./pages/Home";
import { UI } from "./pages/UI";
import { Vacancies } from "./pages/Vacancies";

function App() {
  return (
    <Router>
      <div>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/UI" element={<UI />}/>
          <Route path="/vacancies" element={<Vacancies />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
