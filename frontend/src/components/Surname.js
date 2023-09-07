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

const Surname = ({surname,setSurname}) => {
  return (
    <div>
      <div style={formContainerStyle}>
        <label style={labelStyle}>Surname:</label>
        <input
          type="text"
          value={surname}
          onChange={(e) => setSurname(e.target.value)}
          style={{ marginLeft: "10px" }}
        />
      </div>
    </div>
  );
};



export default Surname;
