import { observer } from 'mobx-react-lite';
import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { useStore } from '../../app/stores/store';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';

const TitleDashboard = observer(() => {
  const { titleStore } = useStore();
  const [newTitleName, setNewTitleName] = useState('');
  const [fieldId, setFieldId] = useState<number | ''>('');

  useEffect(() => {
    titleStore.loadTitles();
  }, [titleStore]);

  const handleCreateTitle = () => {
    if (fieldId !== '') {
      titleStore.createTitle(newTitleName, Number(fieldId)).then(() => {
        setNewTitleName('');
        setFieldId('');
      });
    } else {
      toast.error('Field ID is required');
    }
  };

  const handleDeleteTitle = (titleName: string) => {
    titleStore.deleteTitle(titleName);
  };

  return (
    <LayoutWithSidebar>
      <div className="flex flex-col items-center justify-center p-4 w-full">
        <div className="w-full max-w-2xl px-4 py-5 bg-white shadow-lg rounded-lg">
          <h2 className="text-2xl font-semibold text-center text-gray-800 mb-4">Title Dashboard</h2>
          <div className="mb-8">
            <div className="flex flex-wrap -mx-3 mb-6">
              <div className="w-full px-3 mb-6 md:mb-0">
                <input
                  className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
                  type="text"
                  placeholder="New Title Name"
                  value={newTitleName}
                  onChange={(e) => setNewTitleName(e.target.value)}
                />
                <input
                  className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                  type="number"
                  placeholder="Field ID"
                  value={fieldId}
                  onChange={(e) => setFieldId(Number(e.target.value))}
                />
              </div>
            </div>
            <button
              className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
              type="button"
              onClick={handleCreateTitle}
              disabled={titleStore.submitting}
            >
              Add Title
            </button>
          </div>
          <div className="flow-root">
            <ul className="divide-y divide-gray-200">
              {[...titleStore.titlesRegistry.values()].map((title) => (
                <li key={title.id} className="py-3 sm:py-4">
                  <div className="flex items-center space-x-4">
                    <div className="flex-1 min-w-0">
                      <p className="text-sm font-medium text-gray-900 truncate">{title.titleName}</p>
                      <p className="text-sm text-gray-500 truncate">Field ID: {title.fieldId}</p>
                    </div>
                    <div>
                      <button
                        className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                        type="button"
                        onClick={() => handleDeleteTitle(title.titleName)}
                      >
                        Delete
                      </button>
                    </div>
                  </div>
                </li>
              ))}
            </ul>
          </div>
        </div>
      </div>
    </LayoutWithSidebar>
  );
});

export default TitleDashboard;
