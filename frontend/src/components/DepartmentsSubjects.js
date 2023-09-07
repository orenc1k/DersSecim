import React from "react";
import { Select, MenuItem, FormControl } from "@mui/material";

const formContainerStyle = {
  display: "flex",
  alignItems: "center",
  marginTop: "5px",
};
const labelStyle = {
  fontWeight: "bold",
  marginRight: "10px",
  margin: "0 10px",
};
export const handleClassChange = (selectedClass, setSelectedClass) => {
  setSelectedClass(selectedClass);
};

const DepartmentsSubjects = ({
  availableClasses,
  selectedClass,
  setSelectedClass,
}) => {
  return (
    <div>
      <FormControl sx={{ m: -0.5, minWidth: 10 }} size="small">
        <div style={formContainerStyle}>
          <label style={labelStyle}>Class/Subject:</label>
          <Select
            value={selectedClass}
            onChange={(e) =>
              handleClassChange(e.target.value, setSelectedClass)
            }
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
        </div>
      </FormControl>
    </div>
  );
};

export default DepartmentsSubjects;
