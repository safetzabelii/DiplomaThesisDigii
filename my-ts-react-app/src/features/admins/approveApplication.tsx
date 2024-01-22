import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Button } from 'semantic-ui-react';
import AdministratorStore from '../../app/stores/administratorStore';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';

const ApproveApplication: React.FC = () => {
  const [applications, setApplications] = useState<any[]>([]); // Replace 'any' with the actual type of your thesis applications
  const administratorStore = new AdministratorStore();



  const handleApproveClick = async (thesisApplicationId: number) => {
    try {
      await administratorStore.approveThesisApplication(thesisApplicationId);
      // Update the applications list to reflect the approval status
      const updatedApplications = applications.map((app) => {
        if (app.id === thesisApplicationId) {
          return { ...app, approved: true };
        }
        return app;
      });
      setApplications(updatedApplications);
    } catch (error) {
      // Handle error (e.g., show an error message)
    }
  };

  return (
    <LayoutWithSidebar>
    <div className="bg-gray-100 p-4">
      <h1 className="text-2xl font-bold mb-4">Thesis Applications</h1>
      <div className="grid gap-4">
        {applications.map((application) => (
          <div
            key={application.id}
            className="bg-white rounded-md shadow-md p-4 flex justify-between items-center"
          >
            <div>
              <h2 className="text-lg font-semibold">{application.title}</h2>
              <p className="text-gray-600">{application.studentName}</p>
            </div>
            <div>
              {!application.approved && (
                <Button
                  onClick={() => handleApproveClick(application.id)}
                  className="bg-green-500 hover:bg-green-600 text-white"
                >
                  Approve
                </Button>
              )}
              {application.approved && (
                <span className="text-green-500">Approved</span>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
    </LayoutWithSidebar>
  );
};

export default observer(ApproveApplication);
