import React from "react";
import Button from "@mui/material/Button";
import AddBoxIcon from "@mui/icons-material/AddBox";
import EventAvailableIcon from "@mui/icons-material/EventAvailable";
import { GetSemesterMustCourses,GetSectionDays,GetDepartmentCode} from "./Crud";
import {handleSchedule} from "./Schedule.tsx";

export const handleAddMustCourse = (
  selectedSemester,
  setMustCourses,
  deptCode
) => {
  try {
    debugger;
    GetSemesterMustCourses(deptCode, selectedSemester).then((res) => {
      const mustCourses = res.data.map((course) => course.courseName);
      setMustCourses(mustCourses);
    });
  } catch (error) {
    console.log(error);
  }
};

function splitString(input) {
  const regex = /([A-Za-z]+)([0-9]+):(.+)/;
  const matches = input.match(regex);

  if (matches && matches.length === 4) {
    const [, alphabetic, numeric, remaining] = matches;
    return [alphabetic, numeric, remaining];
  }

  return null; // Return null if the input doesn't match the expected pattern
}
/* handleSchedule = async (time, day, courseCode,schedule,setSchedule,setScheduleTableUpdated)
 */

const handleGetSectionDays = async (addedSubjects,selectedSemester,surname,schedule,setSchedule,setScheduleTableUpdated) => {
  debugger;
  addedSubjects.forEach(async(element) => {
    console.log("elementttt:",splitString(element));
    var deptCode = await GetDepartmentCode(splitString(element)[0]).then((res) => {
      return res.data;
    });
    var subjectCode = splitString(element)[1];

    if (subjectCode.length === 3) {
      subjectCode = "0" + subjectCode;
    }
    deptCode = deptCode.toString();
    debugger;
    await GetSectionDays(deptCode+subjectCode, 2, surname, null).then((result) => {
    result.forEach(async (element) => {
      debugger;
      if (element.time1 && element.day1) {
        await handleSchedule(
          element.time1,
          element.day1,
          element.subjectCode,
          schedule,
          setSchedule,
          setScheduleTableUpdated
        );
      }

      if (element.time2 && element.day2) {
        await handleSchedule(
          element.time2,
          element.day2,
          element.subjectCode,
          schedule,
          setSchedule,
          setScheduleTableUpdated,
        );
      }

      if (element.time3 && element.day3) {
        await handleSchedule(
          element.time3,
          element.day3,
          element.subjectCode,
          schedule,
          setSchedule,
          setScheduleTableUpdated,
        );
      }
    });
    console.log("result", result);
  }); 
  });

};

const Buttons = ({
  selectedSemester,
  setMustCourses,
  deptCode,
  addedSubjects,
  setAddedSubjects,
  surname,
  schedule,
  setSchedule,
  setScheduleTableUpdated
}) => {
  return (
    <div style={{ display: "flex", marginTop: "20px", marginLeft: "100px" }}>
      <Button
        startIcon={<AddBoxIcon />}
        variant="contained"
        onClick={() =>
          handleAddMustCourse(selectedSemester, setMustCourses, deptCode)
        }
        style={{ backgroundColor: "orange", color: "white" }}
      >
        Add Must Course
      </Button>
      <Button
        style={{ marginLeft: "50px" }}
        startIcon={<EventAvailableIcon />}
        variant="contained"
        onClick={()=>{handleGetSectionDays(addedSubjects,selectedSemester,surname,schedule,setSchedule,setScheduleTableUpdated)}}
      >
        Schedule
      </Button>
    </div>
  );
};

export default Buttons;
