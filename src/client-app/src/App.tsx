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
import { HeaderComponent } from "./UI/HeaderComponent";
import { Profile } from "./pages/Profile";
import { CreateVacancy } from "./pages/CreateVacancy";
import { CreateAnalysis } from "./pages/CreateAnalysis";
import { CreateInterview } from "./pages/CreateInterview";
import { Vacancy } from "./pages/Vacancy";

function App() {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchCurrentUser());
  }, []);

  return (
    <Router>
      <div className="bg-neutral-950 w-[100vw] min-h-[100vh] text-white">
        <HeaderComponent />
        <div className="flex mt-4 pb-4 items-center justify-center flex-col gap-4">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/UI" element={<UI />} />
            <Route path="/test" element={<Test />} />
            <Route path="/registration" element={<Registration />} />
            <Route path="/login" element={<Login />} />
            <Route path="/profile" element={<Profile />} />
            <Route path="/vacancies" element={<Vacancies />} />
            <Route path="/vacancies/:id" element={<Vacancy />} />
            <Route path="/vacancies/create" element={<CreateVacancy />} />
            <Route
              path="/vacancies/:id/analyses/create"
              element={<CreateAnalysis />}
            />
            <Route
              path="/vacancies/:id/interviews/create"
              element={<CreateInterview />}
            />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;
