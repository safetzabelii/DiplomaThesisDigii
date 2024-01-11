import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { toast } from 'react-toastify';
import { useStore } from '../../app/stores/store';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';
import { Button, Dropdown } from 'semantic-ui-react';
import departmentStore from '../../app/stores/departmentStore';
import DepartmentStore from '../../app/stores/departmentStore';


const FieldDashboard = observer(() => {
  const { departmentStore } = useStore();
  const { fieldStore } = useStore();
  const [fieldName, setFieldName] = useState('');
  const [departmentId, setDepartmentId] = useState('');

 

  useEffect(() => {
    fieldStore.loadFields();
    departmentStore.loadDepartments();
  }, [fieldStore, departmentStore]);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!fieldName || !departmentId) {
        toast.error("Please provide both a field name and a department ID.");
        return;
    }

    await fieldStore.createField(fieldName, parseInt(departmentId));
    setFieldName('');
    setDepartmentId('');
};

const departmentOptions = departmentStore.departments.map(dept => (
  <option key={dept.id} value={dept.id}>{dept.name}</option>
));


  return (
    <LayoutWithSidebar>
    <div className="container mx-auto p-6">
      <h2 className="text-3xl font-semibold mb-6">Field Dashboard</h2>

      <form onSubmit={handleSubmit} className="mb-8 p-4 bg-white rounded-lg shadow">
        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="fieldName">
            Field Name
          </label>
          <input 
            id="fieldName"
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
            type="text" 
            placeholder="Enter field name" 
            value={fieldName} 
            onChange={(e) => setFieldName(e.target.value)}
          />
        </div>
        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="department">
            Department
          </label>
          <select
            id="department"
            className="block appearance-none w-full bg-white border border-gray-300 text-gray-700 py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            value={departmentId}
            onChange={(e) => setDepartmentId(e.target.value)}
          >
            <option value="" disabled>Select Department</option>
            {departmentOptions}
          </select>
        </div>
        <Button 
          className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
          type="submit"
          loading={fieldStore.submitting}
        >
          Create Field
        </Button>
      </form>

        <div className="bg-white rounded shadow p-4">
          <h3 className="text-xl font-semibold mb-4">Fields</h3>
          <ul className="space-y-3">
            {Array.from(fieldStore.fieldRegistry.values()).map(field => (
              <li key={field.id} className="p-3 hover:bg-gray-100 rounded">
                <p className="text-lg font-medium">{field.fieldName}</p>
                <p className="text-gray-600">Department ID: {field.departmentId}</p>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </LayoutWithSidebar>
  );
});

export default FieldDashboard;
