import React from "react";
import { Select, MenuItem, FormControl } from "@mui/material";

const semesters = ["1", "2", "3", "4", "5", "6", "7", "8"];



const formContainerStyle = {
    display: "flex",
    alignItems: "center", // Center elements vertically
    marginTop: "5px",
    };
const labelStyle = {
    fontWeight: "bold",
    marginRight: "10px",
    margin: "0 10px", // Add margin to both left and right sides of the label
    };
    
const Semester = ({
selectedSemester,
setSelectedSemester,
}) => {
  return (
    <div>
      <FormControl sx={{ m: -0.5, minWidth: 10 }} size="small">
        <div style={formContainerStyle}>
          <label style={labelStyle}>Semester:</label>
          <Select
            value={selectedSemester}
            onChange={(e) => setSelectedSemester(e.target.value)}
            style={{ marginLeft: "10px" }}
          >
            {/* Options for the Semester Select */}
            <MenuItem value="">
              <em>None</em>
            </MenuItem>
            {semesters.map((semester) => (
              <MenuItem key={semester} value={semester}>
                {semester}
              </MenuItem>
            ))}
          </Select>
        </div>
      </FormControl>
    </div>
  );
};

export default Semester;
