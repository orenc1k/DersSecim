import axios from "axios";


export const GetDepartmentsAsync = async () => {
  return await axios.get("https://localhost:7031/api/Department/GetDepartments");
};

export const AddDepartment = async (addedDepartment)=> {
  return await axios.post(
    "https://localhost:7031/api/Department/AddDepartment",
    addedDepartment
  );
};

export const UpdateDepartment = async (updatedDepartment) => {
  return await axios.put(
    "https://localhost:7031/api/Department/UpdateDepartment",
    updatedDepartment
  );
};

export const DeleteDepartment = async (deptCode) => {
  return await axios.delete("https://localhost:7031/api/Department/DeleteDepartment/" + deptCode);
};


export const AddCourse = async (addedCourse) => {
  return await axios.post("https://localhost:7031/api/Subjects/AddSubject", addedCourse);
};

export const GetSubjects = async () => {
  try {
    const response = await axios.get("https://localhost:7031/api/Subjects/GetSubjects");
    return response.data; 
  } catch (error) {
    console.error("Error fetching subjects:", error);
    throw error; 
  }
};