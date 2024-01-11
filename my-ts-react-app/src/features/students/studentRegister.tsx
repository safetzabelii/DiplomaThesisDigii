import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react-lite';
import { CreateStudentDTO } from '../../app/models/DTOS/CreateStudentDTO';
import { useStore } from '../../app/stores/store';
import departmentStore from '../../app/stores/departmentStore';
import fieldStore from '../../app/stores/fieldStore';

const RegisterStudent = () => {
    const { userStore, fieldStore, departmentStore } = useStore();
    const [student, setStudent] = useState<CreateStudentDTO>({
        name: '',
        surname: '',
        dob: new Date(),
        email: '',
        password: '',
        gender: '',
        phone: '',
        address: '',
        ects: 0,
        degreeLevel: '',
        fieldId: 0,
        departmentId: 0,
    });

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    useEffect(() => {
        fieldStore.loadFields();
        departmentStore.loadDepartments();
    }, [fieldStore, departmentStore]);

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError('');
        setSuccess('');
        try {
            await userStore.registerStudent(student);
            setSuccess('Student registered successfully!');
            // Reset form or redirect as needed
        } catch (error) {
            setError('Registration failed. Please try again.');
        }
    };
    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target as HTMLInputElement | HTMLSelectElement;
        setStudent({ ...student, [name]: value });
    };
    
    const fieldsArray = Array.from(fieldStore.fieldRegistry.values());
    const departmentsArray = Array.from(departmentStore.departmentRegistry.values());

    return (
        <div className="max-w-md mx-auto p-4 bg-white shadow-md">
            <form onSubmit={handleSubmit} className="space-y-4">
                <input
                    type="text"
                    name="name"
                    value={student.name}
                    onChange={handleChange}
                    placeholder="Name"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="text"
                    name="surname"
                    value={student.surname}
                    onChange={handleChange}
                    placeholder="Surname"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="date"
                    name="dob"
                    value={student.dob.toISOString().split('T')[0]}
                    onChange={handleChange}
                    placeholder="Date of Birth"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="email"
                    name="email"
                    value={student.email}
                    onChange={handleChange}
                    placeholder="Email"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="password"
                    name="password"
                    value={student.password}
                    onChange={handleChange}
                    placeholder="Password"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                 <input
                    type="text"
                    name="gender"
                    value={student.gender}
                    onChange={handleChange}
                    placeholder="Gender"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="text"
                    name="phone"
                    value={student.phone}
                    onChange={handleChange}
                    placeholder="Phone"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="text"
                    name="address"
                    value={student.address}
                    onChange={handleChange}
                    placeholder="Address"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="number"
                    name="ects"
                    value={student.ects === 0 ? '' : student.ects}
                    onChange={handleChange}
                    placeholder="ECTS"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="text"
                    name="degreeLevel"
                    value={student.degreeLevel}
                    onChange={handleChange}
                    placeholder="Degree Level"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <select
                    name="fieldId"
                    value={student.fieldId ?? ''}
                    onChange={handleChange}
                    className="w-full p-2 border border-gray-300 rounded"
                >
                    <option value="">Select Field</option>
                    {fieldsArray.map(field => (
                        <option key={field.id} value={field.id}>
                            {field.fieldName}
                        </option>
                    ))}
                </select>
                <select
                    name="departmentId"
                    value={student.departmentId ?? ''}
                    onChange={handleChange}
                    className="w-full p-2 border border-gray-300 rounded"
                >
                    <option value="">Select Department</option>
                    {departmentsArray.map(department => (
                        <option key={department.id} value={department.id}>
                            {department.name}
                        </option>
                    ))}
                </select>
                
                <button
                    type="submit"
                    className="w-full bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                >
                    Register Student
                </button>
            </form>
            {error && <p className="text-red-500 text-center mt-3">{error}</p>}
            {success && <p className="text-green-500 text-center mt-3">{success}</p>}
        </div>
    );
};

export default observer(RegisterStudent);
