import React, { useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useStore } from "../../app/stores/store";
import LayoutWithSidebar from "../../app/layout/LayoutWithSidebar";

const FacultyPage = observer(() => {
    const { facultyStore, departmentStore } = useStore();
    const { loadFaculties, facultyRegistry, faculties, loadingInitial: facultyLoading } = facultyStore;
    const { loadDepartments, departments, loadingInitial: departmentLoading } = departmentStore;
    
    const [selectedFaculty, setSelectedFaculty] = useState<number | null>(null);
    const [selectedDepartment, setSelectedDepartment] = useState<number | null>(null);
    useEffect(() => {
        if (facultyRegistry.size <= 1) loadFaculties();
        departmentStore.loadDepartments();
    }, [facultyRegistry.size, loadFaculties, departmentStore]);

    useEffect(() => {
        if (facultyRegistry.size <= 1) loadFaculties();
        loadDepartments();
    }, [facultyRegistry.size, loadFaculties, loadDepartments]);

    if (facultyLoading || departmentLoading) {
        return (
            <div className="flex justify-center items-center h-screen">
                <LoadingComponent content='Loading faculties and departments...' />
            </div>
        );
    }
    const handleFacultySelection = (facultyId: number) => {
        setSelectedFaculty(facultyId);
    };

    const handleDepartmentSelection = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedDepartment(parseInt(event.target.value));
    };

    const assignDepartment = () => {
        console.log(`Assigning department ${selectedDepartment} to faculty ${selectedFaculty}`);
    };
    return (
        <LayoutWithSidebar>
            <div className="container mx-auto p-6">
                <h1 className="text-3xl font-bold text-center mb-6">Faculties</h1>
                <div className="bg-white p-4 rounded-lg shadow-md mb-6">
                    {faculties.map(faculty => (
                        <div key={faculty.id} className="mb-4">
                            <button 
                                className={`text-lg ${selectedFaculty === faculty.id ? 'font-bold' : ''}`}
                                onClick={() => handleFacultySelection(faculty.id)}
                            >
                                {faculty.name}
                            </button>
                        </div>
                    ))}
                </div>
                <div className="bg-white p-4 rounded-lg shadow-md">
                    <h2 className="text-2xl font-semibold mb-4">Assign Department to Faculty</h2>
                    <select 
                        className="p-2 border rounded mb-4" 
                        onChange={handleDepartmentSelection}
                        value={selectedDepartment || ''}
                    >
                        <option value="">Select Department</option>
                        {departments.map(department => (
                            <option key={department.id} value={department.id}>{department.name}</option>
                        ))}
                    </select>
                    <button 
                        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" 
                        onClick={assignDepartment}
                        disabled={!selectedDepartment || !selectedFaculty}
                    >
                        Assign Department
                    </button>
                </div>
            </div>
        </LayoutWithSidebar>
    );
});

export default FacultyPage;
