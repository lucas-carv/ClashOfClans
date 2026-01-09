import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import DataTable from './components/DataTable';
import PerformancePage from './pages/DesempenhoPage';
import WarLogPage from './pages/WarLogPage';
import { obterResumoDeMembros } from './services/api';

function Home() {
  const [resumoMembros, setResumoMembros] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const clanTag = '#2L0UC9R8P';

  const fetchData = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await obterResumoDeMembros(clanTag);
      const formattedData = data.map(item => {
        const newItem = {};
        Object.keys(item).forEach(key => {
          const normalizedKey = key.toLowerCase();
          if (normalizedKey === 'guerrasparticipadasseq') {
            newItem['Quantidade de Guerras'] = item[key];
          } else if (normalizedKey === 'quantidadeataques') {
            newItem['Quantidade de Ataques'] = item[key];
          } else {
            newItem[key] = item[key];
          }
        });
        return newItem;
      });
      setResumoMembros(formattedData);
    } catch (err) {
      console.error(err);
      setError('Falha ao carregar dados do Clash of Clans. Verifique sua conexão ou a API.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <>
      <h2>Análise duas últimas guerras</h2>
      {loading && <div className="loading">Carregando Ataques...</div>}

      {error && (
        <div className="error">
          <p>{error}</p>
          <button onClick={fetchData}>Tentar Novamente</button>
        </div>
      )}

      {!loading && !error && (
        <DataTable
          data={resumoMembros}
          rowStyle={(row) => (row['Quantidade de Ataques'] === 0 && row['Quantidade de Guerras'] === 2) ? { border: '2px solid #ff6b6b' } : {}}
        />
      )}
    </>
  );
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
        </Routes>
      </div>
    </Router>
  );
}

export default App;
