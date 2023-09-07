import React from "react";

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

const CGPA = ({ cgpa, setCGPA }) => {
  return (
    <div>
      <div style={formContainerStyle}>
        <label style={labelStyle}>CGPA:</label>
        <input
          type="text"
          value={cgpa}
          onChange={(e) => setCGPA(e.target.value)}
          style={{ marginLeft: "10px" }}
        />
      </div>
    </div>
  );
};

export default CGPA;
