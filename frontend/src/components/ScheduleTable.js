import React, { useState, useEffect, Suspense } from "react";
import { MenuItem, Select, Button } from "@mui/material";
import AddBoxIcon from "@mui/icons-material/AddBox";
import AddIcon from "@mui/icons-material/Add";
import EventAvailableIcon from "@mui/icons-material/EventAvailable";
import departments from "./Departments";
import timeSlots from "./Timeslots";
import daysOfWeek from "./DaysOfWeek";

const semesters = ["1", "2", "3", "4", "5", "6", "7", "8"];

const ScheduleTable = () => {
  const [schedule, setSchedule] = useState({});
  const [surname, setSurname] = useState("");
  const [department, setDepartment] = useState("");
  const [userDepartment, setUserDepartment] = useState("");
  const [cgpa, setCGPA] = useState("");
  const [selectedSemester, setSelectedSemester] = useState("");
  const [selectedDepartmentFile, setSelectedDepartmentFile] = useState(null);
  const [selectedClass, setSelectedClass] = useState('');
  const [availableClasses, setAvailableClasses] = useState([]);
  const [takenElectiveCourses, setTakenElectiveCourses] = useState([]);
  const [mustCourses, setMustCourses] = useState([]);

  useEffect(() => {
    if (selectedDepartmentFile) {
      // Load the selected department's subject file
      import(`../Departments/${selectedDepartmentFile}.js`).then((module) => {
        // Use the module to set up subjects or perform any other actions
        setAvailableClasses(module.default);
      });
    }

  }, [selectedDepartmentFile]);

  const handleDepartmentChange = (selectedDepartment) => {
    setDepartment(selectedDepartment);
    setSelectedDepartmentFile(`${selectedDepartment.replace(/\s+/g, "")}`);
  };

  const handleCellClick = (time, day) => {
    const updatedSchedule = { ...schedule };
    if (!updatedSchedule[time]) {
      updatedSchedule[time] = {};
    }
    updatedSchedule[time][day] = prompt(`Enter class for ${day} at ${time}`);
    setSchedule(updatedSchedule);
  };

  const handleClassChange = (selectedClass) => {
    setSelectedClass(selectedClass);
    // Perform any necessary actions based on the selected class
  };

const handleAddMustCourse = (selectedSemester) => {
    if (userDepartment) {
        // Dynamically import the mustCourseData function
        import(`../Departments/${userDepartment}MustCourses.tsx`).then((module) => {
            const mustCourseData = module.default; // Correctly imported function
            const mustCourses = mustCourseData({ semester: selectedSemester });
            // Extract course names from the mustCourses array
            const mustCourseNames = mustCourses.map((course) => course.name);

            // Now you have an array of course names from the imported data
            console.log(mustCourseNames);
            setMustCourses(mustCourseNames);
            // Rest of your code for rendering and processing
        });
    } else {
        console.error("userDepartment is not defined.");
    }
};


  

  const handleSchedule = () => {
    // Implement your logic to generate the schedule here
  };

  return (
    <div style={{ display: "flex" }}>
      <div style={{ overflowX: "auto", width: "65%" }}>
        <table style={{ width: "90%", tableLayout: "fixed" }}>
          <thead>
            <tr>
              <th style={{ width: "15%" }}>Time</th>
              {daysOfWeek.map((day) => (
                <th key={day}>{day}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {timeSlots.map((time) => (
              <tr key={time}>
                <td style={{ textAlign: "center" }}>{time}</td>
                {daysOfWeek.map((day) => (
                  <td
                    key={day}
                    onClick={() => handleCellClick(time, day)}
                    style={{
                      border: "1px solid black", // Add border to create a rectangle effect
                      backgroundColor: schedule[time]?.[day]
                        ? "lightblue"
                        : "white",
                      height: "50px", // Adjust the height of the cells
                      width: "80px", // Adjust the width of the cells
                    }}
                  >
                    {schedule[time]?.[day] || ""}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div style={{ flex: 1 }}>
        <div style={{ marginBottom: "10px" }}>
          <label>Surname:</label>
          <input
            type="text"
            value={surname}
            onChange={(e) => setSurname(e.target.value)}
            style={{ marginLeft: "10px" }}
          />
        </div>
        <div style={{ marginBottom: "10px" }}>
          <label>Department:</label>
          <input
            type="text"
            value={userDepartment}
            onChange={(e) => setUserDepartment(e.target.value)}
            style={{ marginLeft: "10px" }}
          />
        </div>
        <div style={{ marginBottom: "10px" }}>
          <label>CGPA:</label>
          <input
            type="text"
            value={cgpa}
            onChange={(e) => setCGPA(e.target.value)}
            style={{ marginLeft: "10px" }}
          />
        </div>
        <div style={{ marginBottom: "10px" }}>
          <label>Semester:</label>
          <Select
            value={selectedSemester}
            onChange={(e) => setSelectedSemester(e.target.value)}
            style={{ marginLeft: "10px" }}
          >
            <MenuItem value="">
              <em>None</em>
            </MenuItem>
            {semesters.map((semester) => (
              <MenuItem key={semester} value={semester}>
                {semester}
              </MenuItem>
            ))}
          </Select>
        </div>
        <div>
          <Button
            startIcon={<AddBoxIcon />}
            variant="contained"
            onClick={() => handleAddMustCourse(selectedSemester)}
            style={{ backgroundColor: "orange", color: "white" }}
          >
            Add Must Course
          </Button>
          <Button
            startIcon={<EventAvailableIcon />}
            variant="contained"
            onClick={handleSchedule}
            style={{ marginLeft: "10px" }}
          >
            Schedule
          </Button>
        </div>

        <h3> Must Courses</h3>
        <ul> {mustCourses.map((course) => <li>{course}</li>)} </ul>
        <h3> Taken Elective Courses</h3>
        <ul> {takenElectiveCourses.map((course) => <li>{course}</li>)} </ul>
        <div style={{ marginBottom: "10px" }}>
          <label>Department:</label>
          <Select
            value={department}
            onChange={(e) => handleDepartmentChange(e.target.value)}
            style={{ marginLeft: "10px" }}
          >
            <MenuItem value="">
              <em>None</em>
            </MenuItem>
            {departments.map((department) => (
              <MenuItem key={department} value={department}>
                {department}
              </MenuItem>
            ))}
          </Select>
        </div>

        <div style={{ marginBottom: "10px" }}>
          <label>Class/Subject:</label>
          <Select
            value={selectedClass}
            onChange={(e) => handleClassChange(e.target.value)}
            style={{ marginLeft: "10px" }}
          >
            <MenuItem value="">
              <em>None</em>
            </MenuItem>
            {availableClasses.map((classItem) => (
              <MenuItem key={classItem} value={classItem}>
                {classItem}
              </MenuItem>
            ))}
          </Select>
          <Button startIcon={<AddIcon />} variant="contained" style={{ marginLeft: "10px" }} onClick={()=> {
            setTakenElectiveCourses([...takenElectiveCourses, selectedClass]);
          }}> 
            Add
          </Button>
        </div>
      </div>
    </div>
  );
};

export default ScheduleTable;
