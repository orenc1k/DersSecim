import React from "react";
import { Button } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";


const TakenElectiveCourses = ({takenElectiveCourses,setTakenElectiveCourses,selectedClass}) => {

    return (
        <div>
            <Button
              style={{ marginLeft: "20px" }}
              startIcon={<AddIcon />}
              variant="contained"
              onClick={() => {
                setTakenElectiveCourses([
                  ...takenElectiveCourses,
                  selectedClass,
                ]);
              }}
            >
              Add
            </Button>
        </div>
    );

};

export default TakenElectiveCourses;