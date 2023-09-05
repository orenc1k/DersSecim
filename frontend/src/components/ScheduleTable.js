import React, { useState, useEffect } from "react";
import { GetDepartmentsAsync } from "./Crud";
import Schedule from "./Schedule.tsx";
import Surname from "./Surname";
import CGPA from "./CGPA";
import UserDepartment from "./UserDepartment";
import Semester from "./Semester";
import Buttons from "./Buttons";
import Departments from "./Departments";
import DepartmentsSubjects from "./DepartmentsSubjects";
import AdvancedSettings from "./AdvancedSettings";
import TakenElectiveCourses from "./TakenElectiveCourses";

const ScheduleTable = () => {
  /*   const [schedule, setSchedule] = useState({});
   */
  const [showAdvancedSettings, setShowAdvancedSettings] = useState(false);

  const [surname, setSurname] = useState("");
  const [department, setDepartment] = useState("");
  const [userDepartment, setUserDepartment] = useState("");
  const [cgpa, setCGPA] = useState("");
  const [selectedSemester, setSelectedSemester] = useState("");
  const [selectedDepartmentFile, setSelectedDepartmentFile] = useState(null);
  const [selectedUserDepartmentFile, setSelectedUserDepartmentFile] =
    useState(null);
  const [selectedClass, setSelectedClass] = useState("");
  const [availableClasses, setAvailableClasses] = useState([]);
  const [takenElectiveCourses, setTakenElectiveCourses] = useState([]);
  const [mustCourses, setMustCourses] = useState([]);
  const [test, setTest] = useState([]);

  useEffect(() => {
    if (selectedDepartmentFile) {
      // Load the selected department's subject file
      import(`../Departments/${selectedDepartmentFile}.js`).then((module) => {
        // Use the module to set up subjects or perform any other actions
        setAvailableClasses(module.default);
      });
    }
    GetDepartmentsAsync()
      .then((test) => {
        // Sort the 'test' array in ascending order
        test.sort((a, b) => a.localeCompare(b));
        // Now 'test' is sorted in ascending order
        setTest(test);
      })
      .catch((error) => {
        console.error("An error occurred:", error);
      });
  }, [selectedDepartmentFile, selectedUserDepartmentFile, selectedSemester]);

  return (
    <div style={{ display: "flex", alignItems: "flex-start" }}>
      <div style={{ flex: 1 }}>
        <h3>
          <Schedule />
        </h3>
      </div>
      <div style={{ flex: 1, marginLeft: "20px" }}>
        <div style={{ display: "flex", flexDirection: "column" }}>
          <div style={{ display: "flex", marginBottom: "10px" }}>
            <Surname surname={surname} setSurname={setSurname} />
            <CGPA cgpa={cgpa} setCGPA={setCGPA} />

            <UserDepartment
              userDepartment={userDepartment}
              setUserDepartment={setUserDepartment}
              setSelectedUserDepartmentFile={setSelectedUserDepartmentFile}
              test={test}
            />

            <Semester
              selectedSemester={selectedSemester}
              setSelectedSemester={setSelectedSemester}
            />
          </div>

          <Buttons
            selectedSemester={selectedSemester}
            userDepartment={userDepartment}
            setMustCourses={setMustCourses}
          />
          <div>
            <h3> Must Courses</h3>
            <ul>
              {mustCourses.map((course) => (
                <li>{course}</li>
              ))}
            </ul>
            <h3 style={{marginLeft:"250px"}}> Taken Elective Courses</h3>
            <ul>
              {takenElectiveCourses.map((course) => (
                <li>{course}</li>
              ))}
            </ul>
          </div>
          <div style={{ display: "flex", marginBottom: "10px" }}>
            <Departments
              department={department}
              setDepartment={setDepartment}
              setSelectedDepartmentFile={setSelectedDepartmentFile}
            />
            <DepartmentsSubjects
              availableClasses={availableClasses}
              selectedClass={selectedClass}
              setSelectedClass={setSelectedClass}
            />
            <TakenElectiveCourses
              takenElectiveCourses={takenElectiveCourses}
              setTakenElectiveCourses={setTakenElectiveCourses}
              selectedClass={selectedClass}
            />
          </div>
          <AdvancedSettings
            showAdvancedSettings={showAdvancedSettings}
            setShowAdvancedSettings={setShowAdvancedSettings}
          />
        </div>
      </div>
    </div>
  );
};

export default ScheduleTable;
/*     if (selectedSemester) {
      const updatedSchedule = { ...schedule };
    
      for (const mustCourse of mustCourses) {
        const section = AEESections.find((s) => s.code === mustCourse);
    
        if (section) {
          console.log("burda");
          console.log("mustCourse", mustCourse);
          console.log("section", section);
    
          // Initialize nested objects if they are undefined
          if (!updatedSchedule[section.time1]) {
            updatedSchedule[section.time1] = {};
          }
          if (!updatedSchedule[section.time2]) {
            updatedSchedule[section.time2] = {};
          }
          if (!updatedSchedule[section.time3]) {
            updatedSchedule[section.time3] = {};
          }
    
          // Now you can safely set properties on the nested objects
          if (section.day1) {
            updatedSchedule[section.time1][section.day1] = section.code;
          }
    
          if (section.day2) {
            updatedSchedule[section.time2][section.day2] = section.code;
          }
    
          if (section.day3) {
            updatedSchedule[section.time3][section.day3] = section.code;
          }
    
          // Update your state with the modified schedule
          schedule = updatedSchedule;
        }
      }
    
      console.log("updatedSchedule", updatedSchedule);
    } */
