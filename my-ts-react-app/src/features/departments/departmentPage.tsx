import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store";
import { useEffect, useState } from "react";
import LayoutWithSidebar from "../../app/layout/LayoutWithSidebar";
import { Department } from "../../app/models/Department";

const DepartmentPage = observer(() => {
  const { departmentStore } = useStore();
  const [newDepartmentName, setNewDepartmentName] = useState('');
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    departmentStore.loadDepartments();
  }, [departmentStore]);

  const handleCreateDepartment = () => {
    departmentStore.createDepartment(newDepartmentName).then(() => {
      setNewDepartmentName('');
      setShowModal(false);
    });
  };

  return (
    <LayoutWithSidebar>
      <div className="flex flex-col items-center justify-center p-8">
        <div className="text-center">
          <h1 className="text-4xl font-bold mb-6">Departments</h1>
          <button
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            onClick={() => setShowModal(true)}
          >
            Add New Department
          </button>
        </div>

        {showModal && (
          <div className="fixed z-10 inset-0 overflow-y-auto">
            <div className="flex items-center justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">
              <div className="fixed inset-0 transition-opacity">
                <div className="absolute inset-0 bg-gray-500 opacity-75"></div>
              </div>
              
              <div className="inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full">
                <div className="bg-white p-4">
                  <h3 className="text-lg leading-6 font-medium text-gray-900">
                    Create a New Department
                  </h3>
                  <div className="mt-2">
                    <input
                      type="text"
                      className="border-2 border-gray-300 p-2 w-full"
                      placeholder="Department Name"
                      value={newDepartmentName}
                      onChange={(e) => setNewDepartmentName(e.target.value)}
                    />
                  </div>
                </div>
                <div className="bg-gray-50 px-4 py-3 sm:flex sm:flex-row-reverse">
                  <button
                    type="button"
                    className="w-full inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-blue-600 text-base font-medium text-white hover:bg-blue-700 focus:outline-none sm:ml-3 sm:w-auto sm:text-sm"
                    onClick={handleCreateDepartment}
                  >
                    Create
                  </button>
                  <button
                    type="button"
                    className="mt-3 w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:text-gray-500 focus:outline-none sm:mt-0 sm:ml-3 sm:w-auto sm:text-sm"
                    onClick={() => setShowModal(false)}
                  >
                    Cancel
                  </button>
                </div>
              </div>
            </div>
          </div>
        )}

        <div className="mt-8 grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
          {departmentStore.departmentsById.map((department: Department) => (
            <div key={department.id} className="bg-white rounded-lg shadow p-4">
              <h3 className="text-lg font-semibold">{department.name}</h3>
            </div>
          ))}
        </div>
      </div>
    </LayoutWithSidebar>
  );
});

export default DepartmentPage;
