import http from 'k6/http';
import { sleep, check } from 'k6';

export const options = {
	stages: [
		{ duration: '10s', target: 500 }, //aumentar 500 usuários em 10s
		{ duration: '20s', target: 500 } //manter 500 usuários por 20s
	],
	thresholds: {
		http_req_duration: ['p(95)<2000'], //95% das requisições devem ser menores que 2s
		http_req_failed: ['rate<0.01'], //Taxa de falhas devem ser menor que 1%
	},
};

export default function () {

	const payload = JSON.stringify({
		startDate: '2024-01-01',
		finalDate: new Date()
	});

	const res = http.post('http://localhost:5150/api/consolidation/processar', payload, {
		headers: {'Content-type' : 'application/json'}
	})

	check(res, {
		'satus é 200': (r) => r.status === 200,
	});

	sleep(1);
}
