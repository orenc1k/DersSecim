import axios from "axios";

export const GetDepartmentsAsync = async () => {
  try {
    const response = await axios.get("https://localhost:7031/api/Department/GetDepartments");
    
    if (response && response.data && Array.isArray(response.data)) {
      const userDepartmentsArray = response.data
        .filter(department => department.deptShortName !== "") 
        .map(department => department.deptShortName);
      return userDepartmentsArray;
    } else {

      console.error("Unexpected response format:", response);
      return [];
    }
  } catch (error) {
    console.error("An error occurred:", error);
    return [];
  }
};

export const GetDepartment = async (deptCode) => {
  try {
      const response = await axios.get("https://localhost:7031/api/Department/GetDepartment/"+ deptCode);
      return response;
  }
  catch (error){
      console.error("An error occurred:", error);
  }

}
export const GetDepartmentCode = async (deptName) => {
    try {
       return  await axios.get("https://localhost:7031/api/Department/GetDepartmentCode/"+ deptName);
    }

    catch (error){
        console.error("An error occurred:", error);
    }
};


export const GetSemesterMustCourses = async (deptCode, semester) => {
    try {
       return  await axios.get("https://localhost:7031/api/MustCourse/GetSemesterMustCourses/"+ deptCode + "/" + semester);
    }

    catch (error){
        console.error("An error occurred:", error);
    }
};

export const GetSectionDays = async (courseCode,cumGPA,surname, courseGrade) => {
    try {
        const apiUrl = `https://localhost:7031/api/SubjectSections/GetSectionDays/
        ${courseCode}?${cumGPA ? `cumGPA=${cumGPA}` : ''}
        ${(cumGPA && surname) ? `&surname=${surname}` : (surname && !cumGPA) ? `surname=${surname}` : ''}
        ${(courseGrade && (cumGPA || surname)) ? `&courseGrade=${courseGrade}` : (courseGrade && (!cumGPA && !surname)) ? `courseGrade=${courseGrade}` : ''}`;

        const response = await axios.get(apiUrl);
    
        return response.data;
    }
    catch (error){
        console.error("An error occurred:", error);
    }
}