import React from "react";
import Button from "@mui/material/Button";
import AddBoxIcon from "@mui/icons-material/AddBox";
import EventAvailableIcon from "@mui/icons-material/EventAvailable";

export const handleAddMustCourse = (
  selectedSemester,
  userDepartment,
  setMustCourses
) => {
  if (userDepartment) {
    // Dynamically import the mustCourseData function
    import(`../Departments/${userDepartment}MustCourses.tsx`).then((module) => {
      const mustCourseData = module.default; // Correctly imported function
      const mustCourses = mustCourseData({ semester: selectedSemester });
      // Extract course names from the mustCourses array
      const mustCourseNames = mustCourses.map((course) => course.code);

      // Now you have an array of course names from the imported data
      console.log("must", mustCourseNames);
      setMustCourses(mustCourseNames);
      // Rest of your code for rendering and processing
    });
  } else {
    console.error("userDepartment is not defined.");
  }
};
export const handleSchedule = () => {
  // Implement your logic to generate the schedule here
  // You can use the schedule state to get the user's selections
};
const Buttons = ({ selectedSemester, userDepartment, setMustCourses }) => {
  return (
    <div style={{ display: "flex", marginTop: "20px",marginLeft:"100px" }}>
      <Button
        startIcon={<AddBoxIcon />}
        variant="contained"
        onClick={() => handleAddMustCourse(selectedSemester, userDepartment, setMustCourses)}
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
