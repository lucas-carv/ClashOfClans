import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import DataTable from './components/DataTable';
import PerformancePage from './pages/DesempenhoPage';
import WarLogPage from './pages/WarLogPage';
import WarDetailsPage from './pages/WarDetailsPage';
import { obterResumoDeMembros } from './services/api';

function Home() {
  // ... existing Home code ...
}

function App() {
  return (
    <Router>
      <div className="App">
        <h1>Clash of Clans</h1>
        <nav className="nav-menu">
          <Link to="/" className="nav-button">ATAQUE DE MEMBROS</Link>
          <Link to="/performance" className="nav-button">DESEMPENHO</Link>
          <Link to="/logs" className="nav-button">WAR LOGS</Link>
        </nav>

        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/performance" element={<PerformancePage />} />
          <Route path="/logs" element={<WarLogPage />} />
          <Route path="/guerra/detalhe/:clanTag/:oponenteTag" element={<WarDetailsPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
