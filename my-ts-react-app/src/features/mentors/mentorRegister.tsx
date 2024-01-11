import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react-lite';
import { CreateMentorDTO } from '../../app/models/DTOS/CreateMentorDTO';
import { useStore } from '../../app/stores/store';

const MentorRegister = () => {
    const { userStore, departmentStore, fieldStore } = useStore();
    const [mentor, setMentor] = useState<CreateMentorDTO>({
        name: '',
        surname: '',
        dob: new Date(),
        email: '',
        password: '',
        gender: '',
        phone: '',
        address: '',
        role: '',
        status: '',
        availability: '',
        departmentId: 0,
        fieldId: 0,
    });

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    useEffect(() => {
        departmentStore.loadDepartments();
        fieldStore.loadFields();
    }, [departmentStore, fieldStore]);

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError('');
        setSuccess('');
        try {
            await userStore.registerMentor(mentor);
            setSuccess('Mentor registered successfully!');
            
        } catch (error) {
            setError('Registration failed. Please try again.');
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target as HTMLInputElement | HTMLSelectElement;
        setMentor({ ...mentor, [name]: value });
    };

    const fieldsArray = Array.from(fieldStore.fieldRegistry.values());
    const departmentsArray = Array.from(departmentStore.departmentRegistry.values());

    return (
        <div className="max-w-md mx-auto p-4 bg-white shadow-md rounded-lg">
        <h2 className="text-xl font-semibold mb-4 text-center">Register Mentor</h2>
        
        <form onSubmit={handleSubmit} className="space-y-4">
                 <input
                    type="text"
                    name="name"
                    value={mentor.name}
                    onChange={handleChange}
                    placeholder="Name"
                    className="w-full p-2 border border-gray-300 rounded"
                />
           
                    <input
                    type="text"
                    name="surname"
                    value={mentor.surname}
                    onChange={handleChange}
                    placeholder="Surname"
                    className="w-full p-2 border border-gray-300 rounded"
                />
            <input
                    type="date"
                    name="dob"
                    value={mentor.dob.toISOString().split('T')[0]}
                    onChange={handleChange}
                    placeholder="Date of Birth"
                    className="w-full p-2 border border-gray-300 rounded"
                />

            <input
                    type="email"
                    name="email"
                    value={mentor.email}
                    onChange={handleChange}
                    placeholder="Email"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="password"
                    name="password"
                    value={mentor.password}
                    onChange={handleChange}
                    placeholder="Password"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                 <input
                    type="text"
                    name="gender"
                    value={mentor.gender}
                    onChange={handleChange}
                    placeholder="Gender"
                    className="w-full p-2 border border-gray-300 rounded"
                />

                <input
                    type="text"
                    name="phone"
                    value={mentor.phone}
                    onChange={handleChange}
                    placeholder="Phone"
                    className="w-full p-2 border border-gray-300 rounded"
                />
                <input
                    type="text"
                    name="address"
                    value={mentor.address}
                    onChange={handleChange}
                    placeholder="Address"
                    className="w-full p-2 border border-gray-300 rounded"
                />
           <input
                    type="text"
                    name="status"
                    value={mentor.status}
                    onChange={handleChange}
                    placeholder="Status"
                    className="w-full p-2 border border-gray-300 rounded"
                />

                <input
                    type="text"
                    name="availability"
                    value={mentor.availability}
                    onChange={handleChange}
                    placeholder="Availability"
                    className="w-full p-2 border border-gray-300 rounded"
                />
            <select
                    name="departmentId"
                    id="departmentId"
                    required
                    value={mentor.departmentId ?? ''}
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
            <select
                    name="fieldId"
                    id="fieldId"
                    required
                    value={mentor.fieldId ?? ''}
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

                <button
                    type="submit"
                    className="w-full bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
                >
                    Register Mentor
                </button>
            </form>
            {error && <p className="text-red-500 text-center mt-3">{error}</p>}
            {success && <p className="text-green-500 text-center mt-3">{success}</p>}
        </div>
    );
};

export default observer(MentorRegister);
