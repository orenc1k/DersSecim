interface Course {
    code: string;
    name: string;
    ects: string;
    credit: string;
    level: string;
    type: string;
}

const courseData = ({ semester }) => {
    if (semester === '1') {
        const firstSemester: Course[] = [
            {
                code: "5720101",
                name: "AEE101",
                ects: "1",
                credit: "0",
                level: "Undergraduate",
                type: "Undergraduate",
            },
            {
                code: "2360119",
                name: "MATH119",
                ects: "7",
                credit: "5",
                level: "Undergraduate",
                type: "Undergraduate",
            },
            {
                code: "2300105",
                name: "PHYS105",
                ects: "6",
                credit: "4",
                level: "Undergraduate",
                type: "Undergraduate",
            },
            {
                code: "2340107",
                name: "CHEM107",
                ects: "6",
                credit: "4",
                level: "Undergraduate",
                type: "Undergraduate",
            },
            {
                code: "5690105",
                name: "CS105",
                ects: "5",
                credit: "3",
                level: "Undergraduate",
                type: "Undergraduate",
            },
            {
                code: "6390101",
                name: "ENG101",
                ects: "6",
                credit: "4",
                level: "Undergraduate",
                type: "Undergraduate",
            },
        ];
        
        return firstSemester;
    }

    if (semester == 2) {
        const secondSemester: Course[] = [
            {
                code: "5720172",
                name: "AEE172",
                ects: "4",
                credit: "3",
                level: "Undergraduate",
                type: "Undergraduate",
            },
            {
                code: "2360120",
                name: "MATH120",
                ects: "7",
                credit: "5",
                level: "Undergraduate",
                type: "Undergraduate",
            },
            {
                code: "2300106",
                name: "PHYS106",
                ects: "6",
                credit: "4",
                level: "Undergraduate",
                type: "Undergraduate",
            },
            {
                code: "5710240",
                name: "CENG240",
                ects: "4",
                credit: "3",
                level: "Undergraduate",
                type: "Undergraduate",
            },
            {
                code: "6390102",
                name: "ENG102",
                ects: "6",
                credit: "4",
                level: "Undergraduate",
                type: "Undergraduate",
            },
        ];

        return secondSemester;
    }

    // Add other semesters here
};

export default courseData;
