import React, { useState } from "react";
import { observer } from "mobx-react-lite";
import FacultyStore from "../../app/stores/facultyStore";
import { useStore } from "../../app/stores/store";
import { toast } from "react-toastify";

const FacultyForm: React.FC = observer(() => {
  const [facultyName, setFacultyName] = useState<string>("");
  const { facultyStore } = useStore();

  const handleCreateFaculty = async () => {
    if (facultyName.trim() === "") {
        toast.error("Faculty name cannot be empty.");
        return;
    }

    const facultyDTO = { facultyName }; 
    await facultyStore.createFaculty(facultyDTO);
    setFacultyName("");
    toast.success("Faculty created successfully");
};



  return (
    <div className="bg-white p-6 rounded-lg shadow-md">
      <h2 className="text-2xl font-semibold mb-4">Create a New Faculty</h2>
      <div className="flex items-center mb-4">
        <input
          type="text"
          placeholder="Faculty Name"
          value={facultyName}
          onChange={(e) => setFacultyName(e.target.value)}
          className="p-2 border rounded w-full mr-2 focus:outline-none focus:ring focus:border-blue-500"
        />
        <button
          onClick={handleCreateFaculty}
          className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:ring focus:border-blue-500"
        >
          Create Faculty
        </button>
      </div>

      {facultyStore.loading ? <p className="text-gray-500">Creating faculty...</p> : null}
    </div>
  );
});

export default FacultyForm;
