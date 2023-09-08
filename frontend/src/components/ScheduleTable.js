import React, { useState, useEffect, useRef } from "react";
import { GetDepartmentsAsync,GetAllCoursesODTU,GetDepartment } from "./Crud";
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
import AddedCourses from "./AddedCourses";

const ScheduleTable = () => {
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
  const [deptCode, setDeptCode] = useState();
  const [scheduleTableUpdated, setScheduleTableUpdated] = useState(false);
  const [allCourses,setAllCourses]= useState([]);
  const fetchAllCoursesCalled = useRef(false);
  const [courseType, setCourseType] = useState("");

  const [addedSubjects, setAddedSubjects] = useState([]); 

  const [schedule, setSchedule] = useState({
    "08:40-9:30": {},
    "09:40-10:30": {},
    "10:40-11:30": {},
    "11:40-12:30": {},
    "12:40-13:30": {},
    "13:40-14:30": {},
    "14:40-15:30": {},
    "15:40-16:30": {},
    "16:40-17:30": {},
    "17:40-18:30": {},
    "18:40-19:30": {},
    "19:40-20:30": {},
  });
  const [scheduleOptions, setScheduleOptions] = useState([schedule]);
  const [currentScheduleIndex, setCurrentScheduleIndex] = useState(0);
  const currentSchedule = scheduleOptions[currentScheduleIndex];


  useEffect(() => {
    if (!fetchAllCoursesCalled.current) {
      fetchAllCourses();
      fetchAllCoursesCalled.current = true;
    }
    if (selectedDepartmentFile) {
      import(`../Departments/${selectedDepartmentFile}.js`).then((module) => {
        setAvailableClasses(module.default);
      });
    }
    GetDepartmentsAsync()
      .then((test) => {
        test.sort((a, b) => a.localeCompare(b));
        setTest(test);
      })
      .catch((error) => {
        console.error("An error occurred:", error);
      });
    setDeptCode(deptCode);
    setScheduleTableUpdated(false);
  }, [
    selectedDepartmentFile,
    selectedUserDepartmentFile,
    selectedSemester,
    deptCode,
    scheduleTableUpdated,
    fetchAllCoursesCalled,
  ]);


  const fetchAllCourses = async () => {
  
  try {
    const result = await GetAllCoursesODTU();
    for (const subject of result.data) {
      const deptResponse = await GetDepartment(parseInt(subject.subjectCode.toString().slice(0, 3)));
      const deptName = deptResponse.data.deptShortName;
      setAllCourses((prevCourses) => [
        ...prevCourses,
        `${deptName}${subject.subjectCode.toString().slice(4)}`+":"+subject.subjectName,
      ]);
    }

  } catch (error) {
    console.error('Error fetching courses:', error);
  } 
  };

  const handleNextSchedule = () => {
    setCurrentScheduleIndex((prevIndex) =>
      prevIndex === scheduleOptions.length - 1 ? 0 : prevIndex + 1
    );
  };
  const handlePreviousSchedule = () => {
    setCurrentScheduleIndex((prevIndex) =>
      prevIndex === 0 ? scheduleOptions.length - 1 : prevIndex - 1
    );
  };
  return (
    <div style={{ display: "flex", alignItems: "flex-start" }}>
      <div style={{ flex: 1 }}>
        <h3>
          <Schedule
            deptCode={deptCode}
            surname={surname}
            cgpa={cgpa}
            setScheduleTableUpdated={setScheduleTableUpdated}
            schedule={schedule}
            setSchedule={setSchedule}
            setScheduleOptions={setScheduleOptions}
            currentSchedule={currentSchedule}
            handleNextSchedule={handleNextSchedule}
            handlePreviousSchedule={handlePreviousSchedule}
          />
        </h3>
      </div>
      <div style={{ flex: 1, marginLeft: "20px" }}>
        <div style={{ display: "flex", flexDirection: "column" }}>
          <div style={{ display: "flex", marginTop: "30px" }}>
            <Surname surname={surname} setSurname={setSurname} />
            <CGPA cgpa={cgpa} setCGPA={setCGPA} />

            <UserDepartment
              userDepartment={userDepartment}
              setUserDepartment={setUserDepartment}
              setSelectedUserDepartmentFile={setSelectedUserDepartmentFile}
              test={test}
              deptCode={deptCode}
              setDeptCode={setDeptCode}
            />

            <Semester
              selectedSemester={selectedSemester}
              setSelectedSemester={setSelectedSemester}
            />
          </div>

          <Buttons
            selectedSemester={selectedSemester}
            setMustCourses={setMustCourses}
            deptCode={deptCode}
            addedSubjects={addedSubjects}
            setAddedSubjects={setAddedSubjects}
            surname={surname}
            schedule={schedule}
            setSchedule={setSchedule}
            setScheduleTableUpdated={setScheduleTableUpdated}
          />
          <div style={{ marginTop: "30px" }}>
            <div style={{ marginLeft: "100px" }}>
              {" "}
              Must Courses
              <ul>
                {mustCourses.map((course) => (
                  <li>{course}</li>
                ))}
              </ul>
              <div style={{ marginLeft: "250px" }}>
                {" "}
                <div style={{ fontWeight: "bold", fontSize: "20px" }}>
                  {" "}
                  Taken Elective Courses
                </div>
                <ul>
                  {takenElectiveCourses.map((course) => (
                    <li>{course}</li>
                  ))}
                </ul>
              </div>
            </div>
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
          <div style={{border:"2px solid gray",borderRadius:"100px"}}>
          <h3 style={{marginLeft:"300px",fontSize:"20px",fontWeight:"bold",marginTop: "20px"}}>
              AddedCourses
            </h3>
            </div>  
          <AddedCourses
          allCourses={allCourses}
          courseType={courseType}
          setCourseType={setCourseType}
          addedSubjects={addedSubjects}
          setAddedSubjects={setAddedSubjects}
          />    
     </div>
      </div>
    </div>
  );
};

export default ScheduleTable;
