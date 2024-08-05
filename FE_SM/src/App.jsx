import React, { useEffect, useState } from "react";
import { BrowserRouter } from "react-router-dom";
import HeaderComponent from "./components/HeaderComponent";
import FooterComponent from "./components/FooterComponent";
import DashBoardCard from "./components/DashBoardCard";
import Calendar from "./components/Calendar";
const App = () => {
  const [students, setStudents] = useState([]);
  const [courses, setCourses] = useState([]);
  const [subjects, setSubjects] = useState([]);
  const [classOS, setClassOS] = useState([]);
  const [enrollments, setEnrollments] = useState([]);
  // Fetch all students
  useEffect(() => {
    // Fetch data from ASP.NET Core all students
    fetch("https://localhost:7215/API/GetAllStudents", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        // Nếu cần, thêm các header khác, ví dụ như Authorization
      },
    })
      .then((res) => res.json())
      .then((data) => setStudents(data))
      .catch((err) => console.error("Error fetching data:", err));
  }, []);
  // Fetch all courses
  useEffect(() => {
    // Fetch data from ASP.NET Core all courses
    fetch("https://localhost:7215/API/GetAllCourses", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        // Nếu cần, thêm các header khác, ví dụ như Authorization
      },
    })
      .then((res) => res.json())
      .then((data) => setCourses(data))
      .catch((err) => console.error("Error fetching data:", err));
  }, []);
  // Fetch all subjects
  useEffect(() => {
    // Fetch data from ASP.NET Core all subjects
    fetch("https://localhost:7215/API/GetAllSubjects", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        // Nếu cần, thêm các header khác, ví dụ như Authorization
      },
    })
      .then((res) => res.json())
      .then((data) => setSubjects(data))
      .catch((err) => console.error("Error fetching data:", err));
  }, []);
  // Fetch all classes
  useEffect(() => {
    // Fetch data from ASP.NET Core all classes
    fetch("https://localhost:7215/API/GetAllClasses", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        // Nếu cần, thêm các header khác, ví dụ như Authorization
      },
    })
      .then((res) => res.json())
      .then((data) => setClassOS(data))
      .catch((err) => console.error("Error fetching data:", err));
  }, []);
  // Fetch all enrollments
  useEffect(() => {
    // Fetch data from ASP.NET Core all enrollments
    fetch("https://localhost:7215/API/GetAllEnrollments", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        // Nếu cần, thêm các header khác, ví dụ như Authorization
      },
    })
      .then((res) => res.json())
      .then((data) => setEnrollments(data))
      .catch((err) => console.error("Error fetching data:", err));
  }, []);

  return (
    <BrowserRouter className="flex">
      <HeaderComponent />
      <div className="lg:m-4 lg:p-4">
        <div className="flex my-2 space-x-4">
          <DashBoardCard
            color="bg-slate-300"
            title="Total Students"
            number={students.length}
          />
          <DashBoardCard
            color="bg-[#f87171]"
            title="Total Courses"
            number={courses.length}
          />
          <DashBoardCard
            color="bg-[#fcd34d]"
            title="Total Subjects"
            number={subjects.length}
          />
          <DashBoardCard
            color="bg-[#a7f3d0]"
            title="Total Classes"
            number={classOS.length}
          />
          <DashBoardCard
            color="bg-[#a5b4fc]"
            title="Total Enrollments"
            number={enrollments.length}
          />
        </div>
      </div>
      <Calendar />
      <FooterComponent />
    </BrowserRouter>
  );
};

export default App;
