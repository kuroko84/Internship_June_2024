import React from "react";
import { Link } from "react-router-dom";
import SearchComponent from "./SearchComponent";

const HeaderComponent = () => {
  return (
    <div>
      <header className="bg-white shadow-md lg:flex mb-3 justify-between items-center">
        <nav className="container lg:flex lg:w-[80%] justify-start px-4 py-2 items-center">
          <Link to="/" className="text-gray-800 font-bold text-xl p-4">
            Student_Management
          </Link>
          <div className="hidden lg:flex gap-4">
            <Link to="/" className="text-gray-700 hover:text-gray-800">
              Home
            </Link>
            <Link to="/course" className="text-gray-700 hover:text-gray-800">
              Course
            </Link>
            <Link to="/student" className="text-gray-700 hover:text-gray-800">
              Student
            </Link>
            <Link to="/subject" className="text-gray-700 hover:text-gray-800">
              Subject
            </Link>
            <Link to="/score" className="text-gray-700 hover:text-gray-800">
              Score
            </Link>
            <Link
              to="/enrollment"
              className="text-gray-700 hover:text-gray-800"
            >
              Enrollment
            </Link>
            <Link
              to="/classOfStudent"
              className="text-gray-700 hover:text-gray-800"
            >
              Class Of Students
            </Link>
          </div>
        </nav>
        <SearchComponent />
      </header>
    </div>
  );
};

export default HeaderComponent;
