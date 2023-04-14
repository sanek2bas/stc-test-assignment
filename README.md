������� ������ make (C#)

������� ������
����������� �������� ������� ������. �� ������� ����� ������ ������ �����. ������ ������ ������������
����� ���������, ���������� � ����� �������� �� ������ �����. �� ������ ���������� ������ ������ ����
��������� ��� �� �����������. ���������� ������ ������� � ������� ���� �������� � ���� ������
�������������� ��������� ������.
��������� ��������� ���������� ��������� ������ �������� ������, ������� ���������� ��������� �
�������� �����. ��������� ������ ��������� ������, ������� ������� ���������� ��������� ������,
�������� ��� �����������. ��� ������ ������ ���������� ������� ��������� � ��������� ������, �����
���� ��������� ��� �������� ��� ���� ������ � ������� �� �������� �� ������� �����. ��������� ������
���������� ������� ���������� �����, ����� ��� ���������� ������ ������ ���� ������ ��� �����������,
� ��������� �������� ���� ����� � ���� �������. ���� ���������� ������� ���������� ����� ����������, ��
��������� ������ ������� ��������� �� ������.
��� ������ ���� �������������� � ������ ��������. ����� ������ ��������� ������ ���� ������ �
������������. ��������� ������ ��������� ������������ ������ �� ������� ����� � ����������
��������� ������.

������� ������
���� � ��������� makefile � ��������� �������:
target1: dependency1_1 dependency1_2 ... dependency1_N
	action1_1
	action1_2
	...
	action1_M
	...
targetX: dependencyX_1 dependencyX_2 ... dependencyX_R
	actionX_1
	...
	actionX_T
��� target1 ... targetX - �������� �����, dependencyK_1 ... dependencyK_N - �������� �����, �� ������� �������
������ K, actionL_1...actionL_N - ��������, ����������� ������� L.
�������� ����� ������� � ������ ������, ����� ���� ������� ��������� � ������ ������������ ������ �����
���� ��� ��������� ��������. �������� ������� � �������� � ���� ��� ������ �������� ��� ��������
��������� � ������ ������.
���������� ����� �� ����� 1000000.

�������� ������
��������� � ����� ���� ������ ������ ���������� � ����� ������������ ������.

������

���������� makefile:
Target1: Target2 Target3
	execute
	update
Target2: Target3
	sort
Target3
	read

������� ��� ���������� Target1 ����� ��������� ���:
make.exe Target1

�� ������ �������:
read
sort
execute
update