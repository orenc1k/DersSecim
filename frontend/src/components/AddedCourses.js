import React, { useEffect, useState, useRef } from "react";
import {
  FormControl,
  Button,
  Select,
  MenuItem,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Collapse,
} from "@mui/material";
import TextField from "@mui/material/TextField";
import Autocomplete from "@mui/material/Autocomplete";
import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import { GetAllMustCoursesODTU } from "./Crud";

const AddedCourses = ({
  allCourses,
  removeCourse,
  courseType,
  setCourseType,
  addedSubjects,
  setAddedSubjects,
}) => {
  const [allMustCourses, setAllMustCourses] = useState([]);
  const [selectACourse, setSelectACourse] = useState(null);
  const [isTableOpen, setIsTableOpen] = useState(false);
  const fetchAllCoursesCalled = useRef(false);

  const formContainerStyle = {
    display: "flex",
    alignItems: "center",
    marginTop: "10px",
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

  const handleAddSubject = () => {
    setAddedSubjects([...addedSubjects, selectACourse]);
    setSelectACourse(null);
    setIsTableOpen(true); 
  };

  const handleDeleteSubject = (subject) => {
    setAddedSubjects(addedSubjects.filter((s) => s !== subject));
  };
  const handleAllMustCourses = async () => {
    console.log("handleAllMustCourses");
    const res = await GetAllMustCoursesODTU();
    console.log(res.data);
    res.data.map((course) => {
      return setAllMustCourses((allMustCourses) => [
        ...allMustCourses,
        course.courseName,
      ]);
    });
  };
  return (
    <div>
      <div style={{ display: "flex" }}>
        <div>
          <FormControl sx={{ m: -0.5, minWidth: 10 }} size="small">
            <div style={{ formContainerStyle }}>
              <label style={{ labelStyle, color: "red" }}>CourseType</label>
              <Select
                value={courseType}
                onChange={(e) => {
                  setCourseType(e.target.value);
                }}
                style={{
                  marginLeft: "10px",
                  marginTop: "20px",
                  width: "100px",
                }}
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
        <div
          style={{ display: "flex", alignItems: "center", marginLeft: "100px" }}
        >
          <Autocomplete
            autoFocus
            value={selectACourse}
            onChange={(e, newValue) => {
              setSelectACourse(newValue);
            }}
            options={
              courseType === "AllCourses"
                ? allCourses.filter((option) => option.toLowerCase())
                : courseType === "Must"
                ? allMustCourses.filter((option) => option.toLowerCase())
                : []
            }
            renderInput={(params) => (
              <TextField
                {...params}
                label="CourseName"
                placeholder="Search..."
                sx={{ fontSize: "16px", padding: "5px", width: "300px" }}
              />
            )}
          />
          {selectACourse && (
            <Button onClick={handleAddSubject} endIcon={<AddIcon />}></Button>
          )}
        </div>
      </div>

      <Collapse in={isTableOpen}>
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Subject</TableCell>
                <TableCell>Action</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {addedSubjects.map((subject, index) => (
                <TableRow key={index}>
                  <TableCell>{subject}</TableCell>
                  <TableCell>
                    <Button
                      onClick={() => {
                        handleDeleteSubject(subject);
                      }}
                      endIcon={<DeleteIcon />}
                    ></Button>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Collapse>
    </div>
  );
};

export default AddedCourses;
