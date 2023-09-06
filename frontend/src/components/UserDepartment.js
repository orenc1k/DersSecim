import React from "react";
import { useEffect } from "react";
import { Select, MenuItem, FormControl } from "@mui/material";
import { GetDepartmentCode } from "./Crud";

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

const UserDepartment = ({
  userDepartment,
  setUserDepartment,
  setSelectedUserDepartmentFile,
  test,
  deptCode,
  setDeptCode,
}) => {
  const handleUserDepartmentChange = (selectedDepartment) => {
    setUserDepartment(selectedDepartment);
    setSelectedUserDepartmentFile(`${selectedDepartment.replace(/\s+/g, "")}`);
    handleDepartmentCode(selectedDepartment);
  };

  const handleDepartmentCode = async (userDepartment) => {
    try {

      const departmentCode = await GetDepartmentCode(userDepartment);
      setDeptCode(departmentCode.data);
      console.log("departmentCode", deptCode);
    } catch (error) {
      // Handle any errors that may occur during the asynchronous operation
      console.error('Error fetching department code:', error);
    }
  };
  useEffect(() => {
    console.log("departmentCode", deptCode);
  }, [deptCode]);

  return (
    <div>
      <FormControl sx={{ m: -0.5, minWidth: 100 }} size="small">
        <div style={formContainerStyle}>
          <label style={labelStyle}>Department:</label>
          <Select
            value={userDepartment}
            onChange={(e) => handleUserDepartmentChange(e.target.value)}
            style={{ marginLeft: "10px" }}
          >
            <MenuItem value="">
              <em>None</em>
            </MenuItem>
            {test.map((Departments) => (
              <MenuItem key={Departments} value={Departments}>
                {Departments}
              </MenuItem>
            ))}
          </Select>
        </div>
      </FormControl>
    </div>
  );
};

export default UserDepartment;
