import React, { ReactNode, useState } from 'react';
import SideNavbar from './navbar';




type LayoutWithSidebarProps = {
    children: ReactNode;
};

const LayoutWithSidebar: React.FC<LayoutWithSidebarProps> = ({ children }) => {
    return (
        <div className="flex h-screen bg-gray-100">
            <SideNavbar />
            <div className="flex-1 pl-64">
                {children}
            </div>
        </div>
    );
};


export default LayoutWithSidebar;
