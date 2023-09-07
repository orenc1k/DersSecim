import React from "react";
import { Select, MenuItem, FormControl } from "@mui/material";

const departments = [
  "Actuarial Science",
  "Aerospace Engineering",
  "Applied Ethics",
  "Archaeometry",
  "Architecture",
  "Area Studies",
  "Asian Studies",
  "Astrophysics",
  "Biochemistry",
  "Bioinformatics",
  "Biology Education",
  "Biology",
  "Biomedical Engineering",
  "Biotechnology",
  "Building Science",
  "Business Administration (International Joint Program)",
  "Business Administration",
  "Cement Engineering",
  "Chemical Engineering",
  "Chemical Oceanography",
  "Chemistry Education",
  "Chemistry",
  "City and Regional Planning",
  "City Planning",
  "Civil Engineering",
  "Cognitive Sciences",
  "Computational Design and Fabrication Technologies in Architecture (International Joint Program)",
  "Computer Education and Instructional Technology",
  "Computer Engineering",
  "Cryptography",
  "Curriculum and Instruction",
  "Cyber Security",
  "Data and Decision Sciences",
  "Data Informatics",
  "Design Research for Interaction (International Joint Program)",
  "Early Childhood Education",
  "Earth System Science",
  "Earthquake Engineering and Engineering Seismology (International Joint Program)",
  "Earthquake Studies",
  "Economics",
  "Educational Administration and Planning",
  "Educational Sciences",
  "Electrical and Electronics Engineering",
  "Elektrik",
  "Elektronik Teknolojisi",
  "Elementary and Early Childhood Education",
  "Elementary Education",
  "Elementary Mathematics Educ.",
  "Elementary Science and Math. E.",
  "Elementary Science Education",
  "Endüstriyel Otomasyon",
  "Engineering Management",
  "Engineering Sciences",
  "English Language Teaching (International Joint Program)",
  "English Language Teaching",
  "English Literature",
  "Environmental Engineering",
  "Environmental Management",
  "Eurasian Studies",
  "European Integration",
  "European Studies",
  "Executive Master of Business  Administration",
  "Family Psychology",
  "Financial Mathematics: Life Insurance Option",
  "Financial Mathematics",
  "Food Engineering",
  "Food Technology",
  "Foreign Language Education",
  "Gender&Women Studies",
  "Geodetic and Geographical Information Technologies",
  "Geological Engineering",
  "Global and International Affairs (International Joint Program)",
  "Grad.Institute Social Sciences",
  "Graduate Program in Conservation of Cultural Heritage",
  "Graduate School of Marine Sciences",
  "Guidance and Psychological Counseling",
  "History of Architecture",
  "History",
  "Human Resources Development In Education",
  "Hydrosystems Engineering",
  "Industrial and Organizational Psychology",
  "Industrial Automation",
  "Industrial Design",
  "Industrial Engineering",
  "Informatics Online",
  "Information Systems",
  "Institute Of Applied Mathematics",
  "International Relations",
  "Latin and North American Studies",
  "Marine Biology and Fisheries",
  "Marine Geology and Geophysics",
  "Mathematics and Science Education",
  "Mathematics Education",
  "Mathematics Education",
  "Mathematics",
  "Mechanical Design and Manufacturing",
  "Mechanical Engineering",
  "Media and Cultural Studies",
  "Medical Informatics",
  "Meslek Yüksekokulu",
  "Metallurgical and Materials Engineering",
  "Micro and Nanotechnology",
  "Middle East Studies",
  "Mining Engineering",
  "Modelling And Simulation",
  "Modern Language (Greek)",
  "Modern Languages (Arabic)",
  "Modern Languages (Chinese)",
  "Modern Languages (English)",
  "Modern Languages (French)",
  "Modern Languages (German)",
  "Modern Languages (Hebrew)",
  "Modern Languages (Italian)",
  "Modern Languages (Japanese)",
  "Modern Languages (Korean)",
  "Modern Languages (Persian)",
  "Modern Languages (Russian)",
  "Modern Languages (Spanish)",
  "Modern Languages (Turkish as a Foreign Languages)",
  "Molecular Biology and Genetics",
  "Multimedia Informatics",
  "Music and Fine Arts",
  "Music and Fine Arts",
  "Music and Fine Arts",
  "Music and Fine Arts",
  "Neuroscience and Neurotechnology",
  "Occupational Health and Safety",
  "Oceanography",
  "Operational Research",
  "Petroleum and Natural Gas Engineering",
  "Philosophy",
  "Phys. Oceanography",
  "Physical Education and Sports",
  "Physics Education",
  "Physics",
  "Political Science and Public Adm.",
  "Polymer Science and Technology",
  "Psychology",
  "Regional Planning",
  "Robotics",
  "Science and Technology Policy Studies",
  "Science Education",
  "Scientific Computing Program",
  "Secondary Science and Mathematics Educ.",
  "Settlement Archaeology",
  "Social Anthropology",
  "Social Policy",
  "Social Sciences (Turkish- German) (International Joint Program)",
  "Sociology",
  "Software Engineering",
  "Software Management",
  "Statistics",
  "Structural Mechanics",
  "Tech. Voc.School",
  "Turkish Language",
  "Urban Design",
  "Urban Policy Planning and Local Governments",
  "Welding Technology",
  "Work Based Learning Studies (International Joint Program)",
];

const formContainerStyle = {
  display: "flex",
  alignItems: "center",
  marginTop: "5px",
  marginLeft: "250px",
  border: "1px solid red",
  borderRadius: "5px",
  padding: "5px",
  backgroundColor: "lightblue",
};
const labelStyle = {
  fontWeight: "bold",
  marginRight: "10px",
  margin: "0 10px",
};

export const handleDepartmentChange = (
  selectedDepartment,
  setDepartment,
  setSelectedDepartmentFile
) => {
  setDepartment(selectedDepartment);
  setSelectedDepartmentFile(`${selectedDepartment.replace(/\s+/g, "")}`);
};

const Departments = ({
  department,
  setDepartment,
  setSelectedDepartmentFile,
}) => {
  return (
    <div>
      <FormControl sx={{ m: -0.5, minWidth: 10 }} size="small">
        <div style={formContainerStyle}>
          <label style={{ labelStyle, color: "orange" }}>Department:</label>
          <Select
            value={department}
            onChange={(e) =>
              handleDepartmentChange(
                e.target.value,
                setDepartment,
                setSelectedDepartmentFile
              )
            }
            style={{ marginLeft: "10px" }}
          >
            <MenuItem value="">
              <em>None</em>
            </MenuItem>
            {departments.map((department) => (
              <MenuItem key={department} value={department}>
                {department}
              </MenuItem>
            ))}
          </Select>
        </div>
      </FormControl>
    </div>
  );
};
export default Departments;
