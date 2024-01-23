import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Button, Card, Loader } from 'semantic-ui-react';
import AdministratorStore from '../../app/stores/administratorStore';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';

const ApproveApplication: React.FC = () => {
    const [administratorStore] = useState(() => new AdministratorStore());
    const [theses, setTheses] = useState<any>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const loadTheses = async () => {
            try {
                setLoading(true);
                const loadedTheses = await administratorStore.getAllThesis();
                setTheses(loadedTheses);
            } catch (e) {
                setError('Failed to load theses.');
            } finally {
                setLoading(false);
            }
        };
        loadTheses();
    }, [administratorStore]);

    const handleApprove = async (thesisApplicationId: number) => {
        // Optimistically set the thesis as approved
        setTheses(theses.map((thesis: any) =>
            thesis.id === thesisApplicationId ? { ...thesis, isApproved: true } : thesis
        ));
    
        try {
            await administratorStore.approveThesisApplication(thesisApplicationId);
            // After a successful API call, you may want to update the dueDate or other properties based on the response
        } catch (e) {
            // If the API call fails, revert the isApproved property
            setTheses(theses.map((thesis: any) =>
                thesis.id === thesisApplicationId ? { ...thesis, isApproved: false } : thesis
            ));
            setError('Failed to approve the application.');
        }
    };

    const isApproved = (thesis: any) => {
        // Check if the thesis has been marked as approved during the current session
        return thesis.isApproved || (thesis.dueDate && new Date(thesis.dueDate) > new Date());
    };

    return (
        <LayoutWithSidebar>
            <div className="p-4 bg-gray-100">
                {theses && theses.length > 0 ? (
                    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
                       {theses.map((thesis: any) => (
                                <Card key={thesis.id} className="bg-white rounded-lg shadow-md overflow-hidden">
                                    <Card.Content>
                                    <Card.Header className="font-semibold text-xl">
                                        Thesis #{thesis.id}
                                    </Card.Header>
                                    <Card.Meta className="text-gray-600">
                                        {thesis.level} - {thesis.titleName}
                                    </Card.Meta>
                                    <Card.Description>
                                        <p className="text-gray-800 mt-2">
                                            <strong>Student:</strong> {thesis.studentName}
                                        </p>
                                        <p className="text-gray-800">
                                            <strong>Mentor:</strong> {thesis.mentorName}
                                        </p>
                                    </Card.Description>
                                </Card.Content>
                               <Card.Content extra>
                               <Button
                                    onClick={() => handleApprove(thesis.id)}
                                    className={`w-full text-white ${isApproved(thesis) ? 'bg-gray-400' : 'bg-green-500 hover:bg-green-600'} focus:outline-none focus:ring-2 focus:ring-green-700 focus:ring-opacity-50`}
                                    disabled={isApproved(thesis)}
                                >
                                    {isApproved(thesis) ? 'Approved' : 'Approve'}
                                </Button>

                                </Card.Content>
                            </Card>
                            ))}
                    </div>
                ) : (
                    <p className="text-center text-gray-600">No theses available for approval.</p>
                )}
            </div>
        </LayoutWithSidebar>
    );
};

export default observer(ApproveApplication);
