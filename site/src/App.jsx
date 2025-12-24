import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import DataTable from './components/DataTable';
import PerformancePage from './pages/PerformancePage';
import { getClans } from './services/api';

function Home() {
  const [clans, setClans] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchData = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await getClans();
      // Transform data to friendly headers
      const formattedData = data.map(item => {
        const newItem = {};
        Object.keys(item).forEach(key => {
          const normalizedKey = key.toLowerCase();

          if (normalizedKey === 'guerrasparticipadasseq') {
            newItem['Sequência de Guerras Participadas'] = item[key];
          } else if (normalizedKey === 'quantidadeataques') {
            newItem['Quantidade de Ataques'] = item[key];
          } else {
            newItem[key] = item[key];
          }
        });
        return newItem;
      });
      setClans(formattedData);
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
      <h2>Resumo de ataque de membros</h2>
      {loading && <div className="loading">Carregando Clãs...</div>}

      {error && (
        <div className="error">
          <p>{error}</p>
          <button onClick={fetchData}>Tentar Novamente</button>
        </div>
      )}

      {!loading && !error && (
        <DataTable data={clans} />
      )}
    </>
  );
}

function App() {
  return (
    <Router>
      <div className="App">
        <h1>Clash of Clans Explorer</h1>
        <nav style={{ marginBottom: '2rem', display: 'flex', gap: '1rem', justifyContent: 'center' }}>
          <Link to="/" style={{ color: 'var(--primary-color)', textDecoration: 'none', fontWeight: 'bold' }}>RESUMO</Link>
          <Link to="/performance" style={{ color: 'var(--primary-color)', textDecoration: 'none', fontWeight: 'bold' }}>DESEMPENHO</Link>
        </nav>

        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/performance" element={<PerformancePage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
