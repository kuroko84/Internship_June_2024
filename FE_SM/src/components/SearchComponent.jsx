import React, { useState, useEffect, useRef } from "react";

const SearchComponent = () => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState([]);
  const [showPopup, setShowPopup] = useState(false);
  const [data, setData] = useState([]); // State để lưu trữ dữ liệu từ API

  // Hàm xử lý tìm kiếm
  const handleSearch = (event) => {
    const value = event.target.value;
    setQuery(value);

    if (value === "") {
      setShowPopup(false);
      setResults([]);
    } else {
      const filteredResults = data.filter((item) =>
        item.name.toLowerCase().includes(value.toLowerCase())
      );
      setResults(filteredResults);
      setShowPopup(true);
    }
  };

  // Hàm xử lý khi chọn một mục trong danh sách
  const handleSelectItem = (item) => {
    setQuery(item.name);
    setShowPopup(false);
  };

  // useRef hook lưu trữ component ref
  const searchRef = useRef(null);

  // Xử lý click ngoài component
  useEffect(() => {
    const handleClickOutside = (event) => {
      if (searchRef.current && !searchRef.current.contains(event.target)) {
        setShowPopup(false);
      }
    };

    document.addEventListener("click", handleClickOutside);

    return () => {
      document.removeEventListener("click", handleClickOutside);
    };
  }, [searchRef]);

  // Fetch data từ API khi component mount
  useEffect(() => {
    fetch("https://localhost:7215/API/GetAllStudents", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((res) => res.json())
      .then((data) => setData(data))
      .catch((err) => console.error("Error fetching data:", err));
  }, []);

  return (
    <div ref={searchRef} className="relative lg:mr-4 w-full lg:max-w-[300px]">
      <input
        type="text"
        placeholder="Search student name..."
        value={query}
        onChange={handleSearch}
        className="w-full p-2 border border-gray-300 rounded"
      />
      {showPopup && results.length > 0 && (
        <div className="absolute left-0 right-0 z-10 bg-white max-h-52 overflow-y-auto rounded">
          {results.map((item) => (
            <div
              key={item.id} // Sử dụng id là khóa duy nhất
              onClick={() => handleSelectItem(item)}
              className="p-2 cursor-pointer hover:bg-gray-200"
            >
              {item.name} {/* Hiển thị tên sinh viên */}
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default SearchComponent;
