


interface Section {
    code: string;
    section: Number;
    day1: string;
    day2: string | null;
    day3: string | null; 
    time1: string;
    time2: string | null;
    time3: string | null;
    room1: string;
    room2: string | null;
    room3: string | null;
}

const sectionData = 
        [
            {
                code: "5720101",
                section: 1,
                day1: "Monday",
                day2: "Wednesday",
                day3: "Friday",
                time1: "08:40-09:30",
                time2: "08:40-09:30",
                time3: "08:40-09:30",
                room1: "B-101",
                room2: "B-101",
                room3: "B-101",
            },
            {
                code: "2360119",
                section: 1,
                day1: "Monday",
                day2: "Wednesday",
                day3: "Friday",
                time1: "13:40-14:30",
                time2: "13:40-14:30",
                time3: "13:40-14:30",
                room1: "B-101",
                room2: "B-101",
                room3: "B-101",
            },
        ] as Section[];



export default sectionData;