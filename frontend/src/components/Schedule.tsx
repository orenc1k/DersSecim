import React, { useState } from "react";
import daysOfWeek from "./DaysOfWeek"; // Import the constants
import timeSlots from "./Timeslots"; // Import the constants
import { GetSectionDays } from "./Crud";
import { Button } from "@mui/material";
import { GetDepartment } from "./Crud";

const ScheduleTable =  ({deptCode, surname, cgpa,setScheduleTableUpdated,schedule,setSchedule}) => {

  const handleSchedule = async (time, day, courseCode) => {
    const updatedSchedule = { ...schedule };
    const courseCodeString = courseCode.toString();

const firstThreeDigits = courseCodeString.slice(0, 3);
const fourthDigit = courseCodeString.charAt(3);
const restOfCode = courseCodeString.slice(4);


const deptName = await GetDepartment(firstThreeDigits).then((result) => {
  return result?.data?.deptShortName;
}
);

const timeIntervals= [] as String[];
let currentTime = time.slice(0,5);
let endTime = time.slice(6);
endTime = `${endTime.slice(0,2)}:40`;

 while (currentTime !== endTime) {
  timeIntervals.push(currentTime+"-"+(parseInt(currentTime.slice(0,2))+1).toString()+":30");

  const [hours, minutes] = currentTime.split(":").map(Number);
  currentTime = `${hours + 1}:40`;
  console.log("current time",currentTime);
} 
console.log("end time",endTime);
timeIntervals.forEach((timeInterval) => {
  console.log("time interval",timeInterval);
}
);

timeIntervals.forEach((timeInterval) => {
  updatedSchedule[timeInterval][day] = `${deptName}${restOfCode}`;
});

setSchedule(updatedSchedule); 
setScheduleTableUpdated(true);
  }

  const handleScheduleGor = () => {
    return console.log("schedule",schedule);
  };

  const handleGetSectionDays = async() => {
    await GetSectionDays(5710111,2,'OR','CC').then((result) => {
      result.forEach(async element => {
        if (element.time1 && element.day1) {

          await handleSchedule(element.time1, element.day1, element.subjectCode);
        }
      
        if (element.time2 && element.day2) {
          await handleSchedule(element.time2, element.day2, element.subjectCode);
        }

        if (element.time3 && element.day3) {
          await handleSchedule(element.time3, element.day3, element.subjectCode);
        }
       });
      console.log("result",result); 
    }
    )
  }
  return ( 
    <div style={{ display: "flex" }}>
      <div style={{ overflowX: "auto", width: "100%" }}>
        <table style={{ width: "100%", tableLayout: "fixed", // Distribute extra space between elements 
        }}>
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
                    // No need for onClick handler if data is already in schedule state
                    style={{
                      border: "1px solid black",
                      backgroundColor: schedule[time]?.[day]
                        ? "lightblue"
                        : "white",
                      height: "50px",
                      width: "80px",
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
      <Button onClick={()=> { handleGetSectionDays()
    // Handle the result here
    // This should display your data
  }
         }>
        Call Subject API
      </Button>

      <Button onClick={()=>{handleScheduleGor()}}> Schedule gör</Button>
    </div>
  );
};

export default ScheduleTable;
