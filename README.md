# TXTWordsCounter
Запросы на PostgreSQL
1. Сотрудника с максимальной заработной платой:

SELECT * FROM employees ORDER BY salary DESC LIMIT 1;

2. Максимальную длину цепочки руководителей по таблице сотрудников:

SELECT MAX(level)
FROM (
  SELECT e1.id, e1.name, COUNT(DISTINCT e2.id) AS level
  FROM employees e1
  JOIN employees e2 ON e1.manager_id = e2.id OR e1.id = e2.id
  GROUP BY e1.id, e1.name
) levels; 

3. Отдел, с максимальной суммарной зарплатой сотрудников:

SELECT d.name, SUM(e.salary) as total_salary
FROM employees e
JOIN departments d ON e.department_id = d.id
GROUP BY d.name
ORDER BY total_salary DESC
LIMIT 1;

4. Сотрудника, чье имя начинается на «Р» и заканчивается на «н»:

SELECT * FROM employees WHERE name LIKE 'Р%н';
