import axios from "axios";

interface Department {
  deptCode: number;
  deptShortName: string;
  deptFullName: string;
}

export const GetDepartmentsAsync = async () => {
  return await axios.get("https://localhost:7031/api/Department/GetDepartments");
};

export const AddDepartment = async (addedDepartment:Department):Promise<Department> => {
  return await axios.post(
    "https://localhost:7031/api/Department/AddDepartment",
    addedDepartment
  );
};

export const UpdateDepartment = async (updatedDepartment:Department):Promise<Department> => {
  return await axios.put(
    "https://localhost:7031/api/Department/UpdateDepartment",
    updatedDepartment
  );
};

export const DeleteDepartment = async (deptCode:Number): Promise<boolean> => {
  return await axios.delete("https://localhost:7031/api/Department/DeleteDepartment/" + deptCode);
};