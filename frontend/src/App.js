
import { Button } from "@mui/material";
import ScheduleTable from "./components/ScheduleTable";
import axios from 'axios';

const add= async ()=>  await axios.post('https://localhost:7031/api/Department/AddDepartment', {
    deptCode: 853,
    deptShortName: '',
    deptFullName: 'City Planning'
  });

function App () {
  return (
    
    <div className="App">
        <ScheduleTable/>
    </div>
  );
}

export default App;