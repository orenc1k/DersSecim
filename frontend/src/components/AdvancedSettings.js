import React from "react";
import { Button } from "@mui/material";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import DoneIcon from "@mui/icons-material/Done";

const AdvancedSettings = ({showAdvancedSettings,setShowAdvancedSettings}) => {

    return (
    <div>
                  {/* Fifth line: Advanced Settings */}
                  <div style={{ width: "200px", marginLeft: "20px" }}>
            <ul style={{ listStyleType: "none", padding: 0 }}>
              <li style={{ marginBottom: "10px" }}>
                <Button
                  variant="contained"
                  endIcon={<ExpandMoreIcon />}
                  style={{ cursor: "pointer" }}
                  onClick={() => setShowAdvancedSettings(!showAdvancedSettings)}
                >
                  {showAdvancedSettings
                    ? "Hide Advanced Settings"
                    : "Advanced Settings"}
                </Button>
              </li>
              {showAdvancedSettings && (
                <>
                  <li style={{ marginBottom: "10px" }}>
                    <label style={{ cursor: "pointer" }}>
                      <input
                        type="checkbox"
                        /*                     checked={checkSurname}
                    onChange={() => setCheckSurname(!checkSurname)} */
                      />
                      Check Surname
                    </label>
                  </li>
                  <li style={{ marginBottom: "10px" }}>
                    <label style={{ cursor: "pointer" }}>
                      <input
                        type="checkbox"
                        /*                     checked={checkDepartment}
                    onChange={() => setCheckDepartment(!checkDepartment)} */
                      />
                      Check Department
                    </label>
                  </li>
                  <li>
                    <label style={{ cursor: "pointer" }}>
                      <input
                        type="checkbox"
                        /*                    checked={checkCollision}
                    onChange={() => setCheckCollision(!checkCollision)} */
                      />
                      Check Collision
                    </label>
                  </li>
                  <Button
                    variant="contained"
                    startIcon={<DoneIcon />}
                    style={{ backgroundColor: "green", color: "white" }}
                  >
                    Done
                  </Button>
                </>
              )}
            </ul>
          </div>
    </div>
    );
};

export default AdvancedSettings;
