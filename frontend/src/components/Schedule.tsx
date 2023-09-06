import React, { useState } from "react";
import daysOfWeek from "./DaysOfWeek"; // Import the constants
import timeSlots from "./Timeslots"; // Import the constants

const ScheduleTable = () => {
  // Initialize the schedule state with your schedule data
  const [schedule, setSchedule] = useState({
    // Define your schedule data here
    "8:40 - 9:30": {  },
    "9:40 - 10:30": {  },
    "10:40 - 11:30": {  },
    "11:40 - 12:30": {  },
    "12:40 - 13:30": {  },
    "13:40 - 14:30": {  },
    "14:40 - 15:30": {  },
    "15:40 - 16:30": {  },
    "16:40 - 17:30": {  },
    "17:40 - 18:30": {  },
    "18:40 - 19:30": {  },
    "19:40 - 20:30": {  },


    // Add more time slots and data as needed

  });

/*   const addCourse = () => {
    // Add your logic here
    const updatedSchedule = { ...schedule };
    updatedSchedule[timeSlots[0]][daysOfWeek[0]] = "AEE173";
    setSchedule(updatedSchedule);
  } */

  // ...
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
    </div>
  );
};

export default ScheduleTable;
