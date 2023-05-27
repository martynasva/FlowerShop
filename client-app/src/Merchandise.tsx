import React, { useEffect, useState } from 'react';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { useNavigate } from 'react-router-dom';
import { Merchandise } from './models/merchandise';
import { List, ListItem, ListItemButton, ListItemText } from '@mui/material';

export function MerchandiseView() {
  const navigate = useNavigate();
  const [merchandises, setMerchandises] = useState<Merchandise[]>([]);

  useEffect(() => {
    const url = 'https://localhost:7190/api/merchandises'
    fetch(url, {
        cache: 'no-cache',
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
      }).then(async response => {
        const responseData = await response.json() as Merchandise[];
        setMerchandises(responseData);
      })
  }, [])

  const merchandiseBoxes = merchandises.map(merchandise => {
    return (
      <ListItem>
        <ListItemButton onClick={() => {
          navigate(`/merchandise/${merchandise.id}`)
        }}>
        <ListItemText primary={`ID: ${merchandise.id} Name: ${merchandise.name} Price: ${merchandise.price}`} />
        </ListItemButton>
      </ListItem>
    )
  })

  return (
      <Container maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
            <Typography>
                Merchandises
            </Typography>
            <List>
              {merchandiseBoxes}
            </List>
        </Box>
      </Container>
  );
}