import React, { useState, useEffect } from "react";

const Calendar = () => {
  const [currentDate, setCurrentDate] = useState(new Date());
  const [selectedDate, setSelectedDate] = useState(null);

  const generateCalendar = (year, month) => {
    const startOfMonth = new Date(year, month, 1);
    const daysInMonth = new Date(year, month + 1, 0).getDate();
    const firstDayOfWeek = startOfMonth.getDay();

    const daysOfWeek = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

    const days = [];

    // Add headers for days of the week
    days.push(
      daysOfWeek.map((day) => (
        <div key={day} className="text-center font-semibold">
          {day}
        </div>
      ))
    );

    // Add empty boxes for days before the first day of the month
    for (let i = 0; i < firstDayOfWeek; i++) {
      days.push(<div key={`empty-${i}`} className="text-center"></div>);
    }

    // Add boxes for each day of the month
    for (let day = 1; day <= daysInMonth; day++) {
      const isToday =
        year === new Date().getFullYear() &&
        month === new Date().getMonth() &&
        day === new Date().getDate();
      days.push(
        <div
          key={day}
          className={`text-center py-2 border cursor-pointer ${
            isToday ? "bg-blue-500 text-white" : ""
          }`}
          onClick={() => handleDayClick(day)}
        >
          {day}
        </div>
      );
    }

    return days;
  };

  const handleDayClick = (day) => {
    const date = new Date(
      currentDate.getFullYear(),
      currentDate.getMonth(),
      day
    );
    const options = {
      weekday: "long",
      year: "numeric",
      month: "long",
      day: "numeric",
    };
    setSelectedDate(date.toLocaleDateString(undefined, options));
  };

  const handlePreviousMonth = () => {
    setCurrentDate((prevDate) => {
      const newDate = new Date(
        prevDate.getFullYear(),
        prevDate.getMonth() - 1,
        1
      );
      return newDate;
    });
  };

  const handleNextMonth = () => {
    setCurrentDate((prevDate) => {
      const newDate = new Date(
        prevDate.getFullYear(),
        prevDate.getMonth() + 1,
        1
      );
      return newDate;
    });
  };

  useEffect(() => {
    // Reset selected date when the current month changes
    setSelectedDate(null);
  }, [currentDate]);

  const year = currentDate.getFullYear();
  const month = currentDate.getMonth();
  const monthNames = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December",
  ];
  const currentMonth = monthNames[month];

  return (
    <div className="lg:w-auto md:w-9/12 sm:w-10/12 m-8">
      <div className="bg-white shadow-lg rounded-lg overflow-hidden">
        <div className="flex items-center justify-between px-3 py-3 bg-gray-700">
          <button className="text-white" onClick={handlePreviousMonth}>
            Previous
          </button>
          <h2 className="text-white">{`${currentMonth} ${year}`}</h2>
          <button className="text-white" onClick={handleNextMonth}>
            Next
          </button>
        </div>
        <div className="grid grid-cols-7 gap-2 p-4">
          {generateCalendar(year, month)}
        </div>
        {selectedDate && (
          <div
            id="myModal"
            className="modal fixed inset-0 flex items-center justify-center z-50"
          >
            <div
              className="modal-overlay absolute inset-0 bg-black opacity-50"
              onClick={() => setSelectedDate(null)}
            ></div>
            <div className="modal-container bg-white w-11/12 md:max-w-md mx-auto rounded shadow-lg z-50 overflow-y-auto">
              <div className="modal-content py-4 text-left px-6">
                <div className="flex justify-between items-center pb-3">
                  <p className="text-2xl font-bold">Selected Date</p>
                  <button
                    className="modal-close px-3 py-1 rounded-full bg-gray-200 hover:bg-gray-300 focus:outline-none focus:ring"
                    onClick={() => setSelectedDate(null)}
                  >
                    ✕
                  </button>
                </div>
                <div className="text-xl font-semibold">{selectedDate}</div>
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default Calendar;
