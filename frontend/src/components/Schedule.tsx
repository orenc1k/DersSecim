import React from "react";
import daysOfWeek from "./DaysOfWeek";
import timeSlots from "./Timeslots";
import { GetSectionDays } from "./Crud";
import { Button } from "@mui/material";
import { GetDepartment } from "./Crud";
import ArrowCircleLeftRoundedIcon from '@mui/icons-material/ArrowCircleLeftRounded';
import ArrowCircleRightRoundedIcon from '@mui/icons-material/ArrowCircleRightRounded';

export const handleSchedule = async (time, day, courseCode,schedule,setSchedule,setScheduleTableUpdated) => {
  const updatedSchedule = { ...schedule };
  const courseCodeString = courseCode.toString();

  const firstThreeDigits = courseCodeString.slice(0, 3);
  const restOfCode = courseCodeString.slice(4);

  const deptName = await GetDepartment(firstThreeDigits).then((result) => {
    return result?.data?.deptShortName;
  });

  const timeIntervals = [] as String[];
  let currentTime = time.slice(0, 5);
  let endTime = time.slice(6);
  endTime = `${endTime.slice(0, 2)}:40`;
let i=0;
  while (currentTime !== endTime) {
    timeIntervals.push(
      currentTime +
        "-" +
        (parseInt(currentTime.slice(0, 2)) + 1).toString() +
        ":30"

    );
    i++;
    if (i>10) break;
    const [hours] = currentTime.split(":").map(Number);
    currentTime = `${hours + 1}:40`;
    console.log("current time", currentTime);
  }
  console.log("end time", endTime);
  timeIntervals.forEach((timeInterval) => {
    console.log("time interval", timeInterval);
  });

  timeIntervals.forEach((timeInterval) => {
    updatedSchedule[timeInterval][day] = `${deptName}${restOfCode}`;
  });

  setSchedule(updatedSchedule);
  setScheduleTableUpdated(true);
};
const ScheduleTable = ({
  deptCode,
  surname,
  cgpa,
  setScheduleTableUpdated,
  schedule,
  setSchedule,
  currentSchedule,
  handleNextSchedule,
  handlePreviousSchedule,
}) => {


  const handleScheduleGor = () => {
    return console.log("schedule", schedule);
  };

  const handleGetSectionDays = async () => {
    await GetSectionDays(5710111, null, "PZ", null).then((result) => {
      result.forEach(async (element) => {
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
            setScheduleTableUpdated
          );
        }

        if (element.time3 && element.day3) {
          await handleSchedule(
            element.time3,
            element.day3,
            element.subjectCode,
            schedule,
            setSchedule,
            setScheduleTableUpdated
          );
        }
      });
      console.log("result", result);
    });
  };

  return (
    <div style={{ display: "flex", flexDirection: "column", alignItems: "center" }}>
      <div style={{ overflowX: "auto", width: "100%" }}>
        <table style={{ width: "100%", tableLayout: "fixed" }}>
          <thead>
            <tr>
              <th style={{ width: "10%" }}>Time</th>
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
                    style={{
                      border: "1px solid black",
                      backgroundColor: currentSchedule[time]?.[day]
                        ? "lightblue"
                        : "white",
                      height: "50px",
                      width: "80px",
                    }}
                  >
                    {currentSchedule[time]?.[day] || ""}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div>
        <Button startIcon={<ArrowCircleLeftRoundedIcon/>} onClick={handlePreviousSchedule}>Previous</Button>
        <Button endIcon={<ArrowCircleRightRoundedIcon/>}  onClick={handleNextSchedule}>Next</Button>
      </div>
      <div>
        <Button
          onClick={() => {
            handleGetSectionDays();
          }}
        >
          Call Subject API
        </Button>
        <Button
          onClick={() => {
            handleScheduleGor();
          }}
        >
          Schedule gör
        </Button>
      </div>
    </div>
  );
  
};

export default ScheduleTable;
