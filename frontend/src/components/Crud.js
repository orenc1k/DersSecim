import axios from "axios";

export const GetDepartmentsAsync = async () => {
  try {
    const response = await axios.get("https://localhost:7031/api/Department/GetDepartments");
    
    if (response && response.data && Array.isArray(response.data)) {
      // Extract the short names from the response data and create an array
      const userDepartmentsArray = response.data
        .filter(department => department.deptShortName !== "") // Filter out empty strings
        .map(department => department.deptShortName);
      return userDepartmentsArray;
    } else {
      // Handle the case where the response data is not as expected
      console.error("Unexpected response format:", response);
      return [];
    }
  } catch (error) {
    // Handle any errors that occurred during the request
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

        // Make the GET request
        const response = await axios.get(apiUrl);
    
        return response.data;
    }
    catch (error){
        console.error("An error occurred:", error);
    }
}