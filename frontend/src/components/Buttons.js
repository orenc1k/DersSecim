import React from "react";
import Button from "@mui/material/Button";
import AddBoxIcon from "@mui/icons-material/AddBox";
import EventAvailableIcon from "@mui/icons-material/EventAvailable";
import { GetSemesterMustCourses } from "./Crud";

export const handleAddMustCourse = (
  selectedSemester,
  setMustCourses,
  deptCode,
) => {
  try {
debugger;
 GetSemesterMustCourses(deptCode,selectedSemester).then((res) => {
   const mustCourses = res.data.map((course) => course.courseName);
   setMustCourses(mustCourses);
  }
    );
    }
  catch(error){
    console.log(error);
  }
};
export const handleSchedule = () => {
  // Implement your logic to generate the schedule here
  // You can use the schedule state to get the user's selections
};
const Buttons = ({ selectedSemester, userDepartment, setMustCourses,deptCode }) => {
  return (
    <div style={{ display: "flex", marginTop: "20px",marginLeft:"100px" }}>
      <Button
        startIcon={<AddBoxIcon />}
        variant="contained"
        onClick={() => handleAddMustCourse(selectedSemester, userDepartment, setMustCourses,deptCode)}
        style={{ backgroundColor: "orange", color: "white" }}
      >
        Add Must Course
      </Button>
      <Button
        style={{ marginLeft: "50px" }}
        startIcon={<EventAvailableIcon />}
        variant="contained"
        onClick={handleSchedule}
      >
        Schedule
      </Button>
    </div>
  );
};

export default Buttons;
