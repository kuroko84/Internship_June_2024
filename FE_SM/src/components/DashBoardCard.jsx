import React from "react";

const DashBoardCard = ({ title, color, number }) => {
  const style = `${color} p-4 rounded-md hover:scale-103 duration-200 flex-grow w-[50%] min-h-[100px]`;
  return (
    <div className={style}>
      <h3 className="lg:text-xl">{title}</h3>
      <p>{number}</p>
    </div>
  );
};

export default DashBoardCard;
