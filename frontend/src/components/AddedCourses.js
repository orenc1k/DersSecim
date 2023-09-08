import React, { useEffect } from "react";
import { Select, MenuItem, FormControl } from "@mui/material"; 
import { GetAllMustCoursesODTU } from "./Crud";

const AddedCourses = ({ courses, removeCourse,courseType,setCourseType }) => {
    const [allMustCourses,setAllMustCourses]=React.useState([]);
    const fetchAllCoursesCalled = React.useRef(false);
    const [selectACourse,setSelectACourse]=React.useState("");
const formContainerStyle = {
display: "flex",
alignItems: "center",
marginTop: "30px",
};
const labelStyle = {

fontWeight: "bold",
marginRight: "10px",
margin: "0 10px",
};

useEffect(() => {
    if (!fetchAllCoursesCalled.current) {
        fetchAllCoursesCalled.current = true;
        handleAllMustCourses();
    }
}, []);

const handleAllMustCourses = async () => {
    console.log("handleAllMustCourses");
    const res = await GetAllMustCoursesODTU();
    console.log(res.data);
    res.data.map((course) => {
       return setAllMustCourses((allMustCourses) => [...allMustCourses, course.courseName]);
    });
}

const handleSelectACourse = (selectedCourse) => {
    setSelectACourse(selectedCourse);
    console.log("selectedCourse",selectedCourse);
}
return (
<div style={{ display: "flex"}}> 

    <div style={{ }}>
    <FormControl sx={{ m: -0.5, minWidth: 10 }} size="small">
        <div style={{formContainerStyle}}>
          <label style={{ labelStyle, color: "orange" }}>CourseType</label>
          <Select 
            value={courseType}
            onChange={(e) =>{setCourseType(e.target.value)}
            }
            style={{ marginLeft: "10px",marginTop:"20px"}}
          >
            <MenuItem value="AllCourses">
              <em>AllCourses</em>
            </MenuItem>
            <MenuItem value="Must">
              <em>Must</em>
            </MenuItem>
            <MenuItem value="Technical">
              <em>Technical</em>
            </MenuItem>
            <MenuItem value="Restricted">
              <em>Restricted</em>
            </MenuItem>
            <MenuItem value="Non-Tech">
              <em>Non-Tech</em>
            </MenuItem>
          </Select>
        </div>
      </FormControl>
      </div>
    <FormControl 
    sx={{ m: -0.5, minWidth: 10 }}
    size="small"
    style={{marginLeft:"100px"}}
    >
      <div style={{formContainerStyle}}>
        <label style={{ labelStyle, color: "orange" }}>CourseName</label>
        <Select
          value={selectACourse}
          onChange={(e) => setSelectACourse(e.target.value)}
          style={{ marginLeft: "10px",marginTop:"20px" }}
        >
          <MenuItem value="">
            <em>None</em>
          </MenuItem>
          {courseType==="AllCourses"? courses.map((course) => (
            <MenuItem key={course} value={course}>
              {course}
            </MenuItem>
            )):courseType==="Must"? allMustCourses.map((course) => (
                <MenuItem key={course} value={course}>
                    {course}
                </MenuItem>
                )):null}
        </Select>
      </div>
    </FormControl>
    
</div>
);
};


export default AddedCourses;