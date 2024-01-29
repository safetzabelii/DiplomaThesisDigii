import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useStore } from "../../app/stores/store";
import LayoutWithSidebar from "../../app/layout/LayoutWithSidebar";
import FacultyForm from "./facultyForm";
import { toast } from "react-toastify";

const FacultyPage = observer(() => {
  const { facultyStore } = useStore();
  const { loadFaculties, faculties, loadingInitial: facultyLoading, deleteFaculty } = facultyStore;

  useEffect(() => {
    if (faculties.length === 0) loadFaculties();
  }, [faculties.length, loadFaculties]);

  const handleDelete = async () => {
    await deleteFaculty();
    toast.success("Faculty deleted successfully");
  };

  if (facultyLoading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <LoadingComponent content="Loading faculties..." />
      </div>
    );
  }

  return (
    <LayoutWithSidebar>
      <div className="container mx-auto p-6">
        <h1 className="text-3xl font-bold text-center mb-6">Faculties</h1>
        <div className="bg-white p-6 rounded-lg shadow-md mb-6">
          <FacultyForm />
        </div>

        <div className="bg-white p-6 rounded-lg shadow-md">
          {faculties.length === 0 ? (
            <p className="text-lg text-gray-500">No faculties found.</p>
          ) : (
            <ul>
              {faculties.map((faculty) => (
                <li key={faculty.id} className="flex items-center justify-between border-b border-gray-300 py-3">
                  <span className="text-lg">{faculty.name}</span>
                  <button
                    className="text-red-500 hover:text-red-700"
                    onClick={() => handleDelete()}
                  >
                    Delete
                  </button>
                </li>
              ))}
            </ul>
          )}
        </div>
      </div>
    </LayoutWithSidebar>
  );
});

export default FacultyPage;
