import React from "react";
import { Select, MenuItem, FormControl } from "@mui/material"; 


const AddedCourses = ({ courses, removeCourse,courseType,setCourseType }) => {
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

return (
<div style={{ display: "flex"}}> 

    <div style={{ }}>
    <FormControl sx={{ m: -0.5, minWidth: 10 }} size="small">
        <div style={{formContainerStyle}}>
          <label style={{ labelStyle, color: "orange" }}>CourseType</label>
          <Select 
            value={courseType}
            onChange={(e) =>{setCourseType(e.target.value);console.log(e.target.value)}
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
        <label style={{ labelStyle, color: "orange" }}>Added Courses</label>
        <Select
          value={courses}
          onChange={(e) => removeCourse(e.target.value)}
          style={{ marginLeft: "10px" }}
        >
          <MenuItem value="">
            <em>None</em>
          </MenuItem>
          {courses.map((course) => (
            <MenuItem key={course} value={course}>
              {course}
            </MenuItem>
          ))}
        </Select>
      </div>
    </FormControl>
    
</div>
);
};


export default AddedCourses;